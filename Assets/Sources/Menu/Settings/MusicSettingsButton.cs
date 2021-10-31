using UniRx;

namespace Menu.Settings
{
    public class MusicSettingsButton : AudioSettingsButton
    {
        public override IReadOnlyReactiveProperty<bool> Enabled => AudioStatus.MusicEnabled;

        protected override void OnButtonClicked()
        {
            AudioSettingsOperations.SetActiveMusic(!Enabled.Value);
        }
    }
}