namespace CarSumo.Audio.Services
{
    public interface IAudioPreferences
    {
        void Init();
        void ChangeMusicVolume(float volume);
        void ChangeSfxVolume(float volume);
    }
}