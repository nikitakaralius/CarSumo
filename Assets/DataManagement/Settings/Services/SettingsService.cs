using CarSumo.DataManagement.Core;
using CarSumo.GameSettings.Structs;

namespace CarSumo.GameSettings.Services
{
    public class SettingsService : DataService<IReadOnlySettings, AllSettings>
    {
        public SettingsService(IFileService fileService, string rootDirectory) : base(fileService, rootDirectory)
        {
        }

        protected override string FileName => "Settings.JSON";

        protected override IReadOnlySettings EnsureInitialized()
        {
            return new AllSettings()
            {
                Sound = new SoundSettings() {MusicVolume = 1.0f, SfxVolume = 1.0f},
                Localization = new LocalizationSettings() {Text = Language.Eng}
            };
        }
    }
}