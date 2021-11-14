using System.Threading.Tasks;
using CarSumo.DataModel.Settings;
using DataModel.DataPersistence;
using DataModel.GameData.GameSave;
using DataModel.GameData.Settings;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;
using Zenject;

namespace Infrastructure.Initialization
{
    public class AudioSettingsInitialization : IAsyncInitializable
    {
        private const string AudioMixer = "AudioMixer";
        
        private readonly DiContainer _container;
        private readonly IAudioConfiguration _configuration;
        private readonly IAsyncFileService _fileService;

        public AudioSettingsInitialization(DiContainer container,
                                           IAudioConfiguration configuration,
                                           IAsyncFileService fileService)
        {
            _container = container;
            _configuration = configuration;
            _fileService = fileService;
        }
        
        public async Task InitializeAsync()
        {
            AudioMixer mixer = await Addressables.LoadAssetAsync<AudioMixer>(AudioMixer).Task;
            SerializableAudioSettings serializableSettings = await LoadSerializableAudioSettingsAsync() ?? EnsureCreated();
            GameAudioSettings audioSettings = InitializeAudioSettings(mixer, _configuration, serializableSettings);

            BindAudioSettingsInterfaces(audioSettings);
            BindAudioSettingsSave();
        }

        private void BindAudioSettingsSave()
        {
            _container
                .Bind<AudioSettingsSave>()
                .FromNew()
                .AsSingle()
                .NonLazy();

            _container.Resolve<AudioSettingsSave>();
        }

        private void BindAudioSettingsInterfaces(GameAudioSettings audioSettings)
        {
            _container
                .BindInterfacesAndSelfTo<GameAudioSettings>()
                .FromInstance(audioSettings)
                .AsSingle()
                .NonLazy();
        }

        private GameAudioSettings InitializeAudioSettings(AudioMixer mixer,
                                                          IAudioConfiguration configuration,
                                                          SerializableAudioSettings settings)
        {
            return new GameAudioSettings(mixer, configuration, settings.MusicVolume, settings.SfxVolume);
        }

        private async Task<SerializableAudioSettings> LoadSerializableAudioSettingsAsync()
        {
            string path = _configuration.AudioFilePath;
            return await _fileService.LoadAsync<SerializableAudioSettings>(path);
        }

        private SerializableAudioSettings EnsureCreated()
        {
            return new SerializableAudioSettings()
            {
                MusicVolume = GameAudioSettings.Enabled,
                SfxVolume = GameAudioSettings.Enabled
            };
        }
    }
}