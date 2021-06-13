namespace CarSumo.Audio.AudioData
{
    public struct AudioCueKey
    {
        public static AudioCueKey Invalid = new AudioCueKey(-1, null);

        internal int Value { get; set; }
        internal AudioCue AudioCue { get; set; }

        internal AudioCueKey(int value, AudioCue audioCue)
        {
            Value = value;
            AudioCue = audioCue;
        }

        public override bool Equals(object obj)
        {
            return obj is AudioCueKey x && Value == x.Value && AudioCue == x.AudioCue;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode() ^ AudioCue.GetHashCode();
        }

        public static bool operator ==(AudioCueKey a, AudioCueKey b)
        {
            return a.Value == b.Value && a.AudioCue == b.AudioCue;
        }

        public static bool operator !=(AudioCueKey a, AudioCueKey b) => !(a == b);
    }
}