using System.Linq;

namespace DataModel.DataPersistence.Extensions
{
	public static class StringExtensions
	{
		public static string ChangeFileExtensionTo(this string path, string newExtension) => 
			path.Replace(path.Split('.').Last(), newExtension);
	}
}