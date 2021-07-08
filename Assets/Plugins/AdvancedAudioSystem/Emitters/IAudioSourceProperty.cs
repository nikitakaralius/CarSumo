namespace AdvancedAudioSystem.Emitters
{
    public interface IAudioSourceProperty
    {
        bool IsPlaying { get; }
        float Volume { get; set; }
        float Pitch { get; set; }
    }
}
