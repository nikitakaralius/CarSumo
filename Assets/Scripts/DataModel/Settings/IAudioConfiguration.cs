using UnityEngine.Audio;

namespace CarSumo.DataModel.Settings
{
    public interface IAudioConfiguration
    {
        AudioMixer AudioMixer { get; }
        string MusicVolumeParameter { get; }
        string SfxVolumeParameter { get; }
        string AudioFilePath { get; }
    }
}