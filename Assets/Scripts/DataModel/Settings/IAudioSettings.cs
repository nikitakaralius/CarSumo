using UniRx;

namespace CarSumo.DataModel.Settings
{
    public interface IAudioSettings
    {
        IReadOnlyReactiveProperty<float> MusicVolume { get; }
        IReadOnlyReactiveProperty<float> SfxVolume { get; }
    }
}