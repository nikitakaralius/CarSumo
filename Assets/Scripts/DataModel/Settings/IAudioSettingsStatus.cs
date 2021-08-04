using UniRx;

namespace CarSumo.DataModel.Settings
{
    public interface IAudioSettingsStatus
    {
        IReadOnlyReactiveProperty<bool> MusicEnabled { get; }
        IReadOnlyReactiveProperty<bool> SfxEnabled { get; }
    }
}