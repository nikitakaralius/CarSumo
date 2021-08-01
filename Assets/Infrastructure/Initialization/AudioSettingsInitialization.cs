using System.Threading.Tasks;
using CarSumo.DataModel.Settings;
using DataModel.FileData;
using DataModel.GameData.GameSave;
using DataModel.GameData.Settings;
using Zenject;

namespace Infrastructure.Initialization
{
    public class AudioSettingsInitialization : IAsyncInitializable
    {
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
            SerializableAudioSettings serializableSettings = await LoadSerializableAudioSettings() ?? EnsureCreated();
            GameAudioSettings audioSettings = InitializeAudioSettings(_configuration, serializableSettings);
            
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
        }

        private void BindAudioSettingsInterfaces(GameAudioSettings audioSettings)
        {
            _container
                .BindInterfacesAndSelfTo<GameAudioSettings>()
                .FromInstance(audioSettings)
                .AsSingle()
                .NonLazy();
        }

        private GameAudioSettings InitializeAudioSettings(IAudioConfiguration configuration, SerializableAudioSettings settings)
        {
            return new GameAudioSettings(configuration, settings.MusicVolume, settings.SfxVolume);
        }

        private async Task<SerializableAudioSettings> LoadSerializableAudioSettings()
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