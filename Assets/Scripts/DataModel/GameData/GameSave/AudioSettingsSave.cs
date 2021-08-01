using System;
using DataModel.FileData;
using CarSumo.DataModel.Settings;

namespace DataModel.GameData.GameSave
{
    public class AudioSettingsSave : IDisposable
    {
        private readonly IAudioSettings _audioSettings;
        private readonly IFileService _fileService;
        private readonly IAudioConfiguration _configuration;

        public AudioSettingsSave(IAudioSettings audioSettings, IFileService fileService, IAudioConfiguration configuration)
        {
            _fileService = fileService;
            _configuration = configuration;
            _audioSettings = audioSettings;
        }

        public void Dispose()
        {
            Save();
        }

        private void Save()
        {
            string filePath = _configuration.AudioFilePath;
            SerializableAudioSettings settings = ToSerializableAudioSettings(_audioSettings);
            _fileService.Save(settings, filePath);
        }

        private SerializableAudioSettings ToSerializableAudioSettings(IAudioSettings audioSettings)
        {
            return new SerializableAudioSettings()
            {
                MusicVolume = audioSettings.MusicVolume.Value,
                SfxVolume = audioSettings.SfxVolume.Value
            };
        }
    }
}