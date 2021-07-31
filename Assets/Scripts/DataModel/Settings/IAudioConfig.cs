namespace DataModel.Settings
{
    public interface IAudioConfig
    {
        string MusicVolumeParameter { get; }
        string SfxVolumeParameter { get; }
        string FilePath { get; }
    }
}