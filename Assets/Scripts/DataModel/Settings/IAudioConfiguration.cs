namespace DataModel.Settings
{
    public interface IAudioConfiguration
    {
        string MusicVolumeParameter { get; }
        string SfxVolumeParameter { get; }
        string FilePath { get; }
    }
}