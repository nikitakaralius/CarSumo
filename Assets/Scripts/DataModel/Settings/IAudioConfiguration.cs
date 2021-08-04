namespace CarSumo.DataModel.Settings
{
    public interface IAudioConfiguration
    {
        string MusicVolumeParameter { get; }
        string SfxVolumeParameter { get; }
        string AudioFilePath { get; }
    }
}