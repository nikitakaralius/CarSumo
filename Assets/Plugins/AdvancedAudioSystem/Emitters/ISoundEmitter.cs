namespace AdvancedAudioSystem.Emitters
{
    public interface ISoundEmitter
    {
        void Play(AudioCue audioCue);
        void Stop();
    }
}
