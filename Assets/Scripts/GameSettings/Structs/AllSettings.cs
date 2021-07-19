namespace CarSumo.GameSettings.Structs
{
    public class AllSettings
    {
        public SoundSettings Sound { get; }
        public LocalizationSettings Localization { get; }

        public AllSettings(SoundSettings sound, LocalizationSettings localization)
        {
            Sound = sound;
            Localization = localization;
        }
    }
}