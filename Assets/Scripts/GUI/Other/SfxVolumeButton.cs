namespace CarSumo.GUI.Other
{
    public class SfxVolumeButton : SoundVolumeButton
    {
        protected override float Volume => AudioPreferences.Sound.SfxVolume;
        
        protected override void ChangeVolume(float volume)
        {
            AudioPreferences.ChangeSfxVolume(volume);
        }
    }
}