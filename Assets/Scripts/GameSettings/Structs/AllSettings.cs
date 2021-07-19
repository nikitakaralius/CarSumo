namespace CarSumo.GameSettings.Structs
{
    public class AllSettings : IReadOnlySettings
    {
        public SoundSettings Sound { get; set; }
        public LocalizationSettings Localization { get; set; }
    }

    public interface IReadOnlySettings
    {
        SoundSettings Sound { get; }
        LocalizationSettings Localization { get; }
    }
}