using UniRx;

namespace Menu.Settings
{
    public class SfxSettingsButton : AudioSettingsButton
    {
        public override IReadOnlyReactiveProperty<bool> Enabled => AudioStatus.SfxEnabled;
        
        protected override void OnButtonClicked()
        {
            AudioSettingsOperations.SetActiveSfxVolume(!Enabled.Value);            
        }
    }
}