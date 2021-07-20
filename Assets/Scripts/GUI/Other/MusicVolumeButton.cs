namespace CarSumo.GUI.Other
{
    public class MusicVolumeButton : SoundVolumeButton
    {
        protected override float Volume => AudioPreferences.Sound.MusicVolume;
        
        protected override void ChangeVolume(float volume)
        {
            AudioPreferences.ChangeMusicVolume(volume);
        }
    }
}