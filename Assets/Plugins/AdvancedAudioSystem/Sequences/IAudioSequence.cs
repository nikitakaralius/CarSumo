namespace AdvancedAudioSystem.Sequences
{
    public interface IAudioSequence
    {
        AudioClipsMember NextClipMember(AudioClips clips);
    }
}
