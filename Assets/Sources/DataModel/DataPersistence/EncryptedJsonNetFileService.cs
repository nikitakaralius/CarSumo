using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using DataModel.DataPersistence.Extensions;
using Newtonsoft.Json;

namespace DataModel.DataPersistence
{
	public class EncryptedJsonNetFileService : IFileService, IAsyncFileService
	{
		private const string KeysExtension = "keys";
		private const char KeysSeparator = '*'; 

		public TModel Load<TModel>(string path)
		{
			EncryptedFilesData encryptedFilesData = LoadEncrypted(path);
			string json = encryptedFilesData.Corrupted ? string.Empty : Decrypt(encryptedFilesData);
			return JsonConvert.DeserializeObject<TModel>(json);
		}

		public async Task<TModel> LoadAsync<TModel>(string path)
		{
			EncryptedFilesData encryptedFilesData = await LoadEncryptedAsync(path);
			string json = encryptedFilesData.Corrupted ? string.Empty : await DecryptAsync(encryptedFilesData);
			return JsonConvert.DeserializeObject<TModel>(json);
		}

		public void Save<TModel>(TModel model, string path)
		{
			string json = JsonConvert.SerializeObject(model);

			using (var aes = Aes.Create())
			{
				aes.GenerateKey();
				aes.GenerateIV();

				ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

				byte[] encrypted = Encrypt(json, encryptor);

				using (var streamWriter = new StreamWriter(path))
				{
					streamWriter.Write(BitConverter.ToString(encrypted));
				}
				using (var streamWriter = new StreamWriter(KeysFile(path)))
				{
					streamWriter.Write(KeysFileData(aes));
				}
			}
		}
		
		public async Task SaveAsync<TModel>(TModel model, string path)
		{
			string json = JsonConvert.SerializeObject(model);

			using (var aes = Aes.Create())
			{
				aes.GenerateKey();
				aes.GenerateIV();

				ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

				byte[] encrypted = await EncryptAsync(json, encryptor);

				using (var streamWriter = new StreamWriter(path))
				{
					await streamWriter.WriteAsync(BitConverter.ToString(encrypted));
				}
				using (var streamWriter = new StreamWriter(KeysFile(path)))
				{
					await streamWriter.WriteAsync(KeysFileData(aes));
				}
			}
		}

		private static string KeysFileData(Aes aes) => $"{BitConverter.ToString(aes.Key)}{KeysSeparator}{BitConverter.ToString(aes.IV)}";

		public static string KeysFile(string path) => path.ChangeFileExtensionTo(KeysExtension);

		private static byte[] Encrypt(string json, ICryptoTransform encryptor)
		{
			byte[] bytes;
			
			using (var memoryStream = new MemoryStream())
			{
				using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
				{
					using (var streamWriter = new StreamWriter(cryptoStream))
					{
						streamWriter.Write(json);
					}
				}
				bytes = memoryStream.ToArray();
			}

			return bytes;
		}

		private async Task<byte[]> EncryptAsync(string json, ICryptoTransform encryptor)
		{
			byte[] bytes;
			
			using (var memoryStream = new MemoryStream())
			{
				using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
				{
					using (var streamWriter = new StreamWriter(cryptoStream))
					{
						await streamWriter.WriteAsync(json);
					}
				}
				bytes = memoryStream.ToArray();
			}

			return bytes;
		}

		private readonly struct EncryptedFilesData
		{
			private readonly string _model;
			private readonly string _key;
			private readonly string _iv;

			public EncryptedFilesData(string model, string keys)
			{
				_model = model;
				string[] splitKeys = keys.Split(KeysSeparator);
				
				_key = splitKeys.Length > 1 ? splitKeys[0] : string.Empty;
				_iv = splitKeys.Length > 1 ? splitKeys[1] : string.Empty;
			}

			public bool Corrupted => string.IsNullOrEmpty(_model);

			public byte[] Model => ConvertBinaryFile(_model);

			public byte[] Key => ConvertBinaryFile(_key);

			public byte[] IV => ConvertBinaryFile(_iv);
			
			private static byte[] ConvertBinaryFile(string binary) => Array.ConvertAll(binary.Split('-'), x => Convert.ToByte(x, 16));
		}

		private static EncryptedFilesData LoadEncrypted(string path)
		{
			string model;
			using (var streamReader = new StreamReader(path))
			{
				model = streamReader.ReadToEnd();
			}
			string keys;
			using (var streamReader = new StreamReader(KeysFile(path)))
			{
				keys = streamReader.ReadToEnd();
			}

			return new EncryptedFilesData(model, keys);
		}
		
		private static async Task<EncryptedFilesData> LoadEncryptedAsync(string path)
		{
			string model;
			using (var streamReader = new StreamReader(path))
			{
				model = await streamReader.ReadToEndAsync();
			}
			string keys;
			using (var streamReader = new StreamReader(KeysFile(path)))
			{
				keys = await streamReader.ReadToEndAsync();
			}

			return new EncryptedFilesData(model, keys);
		}

		private static string Decrypt(EncryptedFilesData encrypted)
		{
			string decrypted;
			using (var aes = Aes.Create())
			{
				aes.Key = encrypted.Key;
				aes.IV = encrypted.IV;

				ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

				using (var memoryStream = new MemoryStream(encrypted.Model))
				{
					using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
					{
						using (var cryptoStreamWriter = new StreamReader(cryptoStream))
						{
							decrypted = cryptoStreamWriter.ReadToEnd();
						}
					}
				}
			}
			return decrypted;
		}
		
		private static async Task<string> DecryptAsync(EncryptedFilesData encrypted)
		{
			string decrypted;
			using (var aes = Aes.Create())
			{
				aes.Key = encrypted.Key;
				aes.IV = encrypted.IV;

				ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

				using (var memoryStream = new MemoryStream(encrypted.Model))
				{
					using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
					{
						using (var cryptoStreamWriter = new StreamReader(cryptoStream))
						{
							decrypted = await cryptoStreamWriter.ReadToEndAsync();
						}
					}
				}
			}
			return decrypted;
		}
	}
}