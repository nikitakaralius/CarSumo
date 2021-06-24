namespace CarSumo
{
    public static class Converter
    {
        public static float Map(float value, float originMin, float originMax, float newMin, float newMax)
        {
            return (value - originMin) * (newMax - newMin) / (originMax - originMin) + newMin;
        }

        public static float MapByPercentsRange(float value, float min, float max)
        {
            return Map(value, 0.0f, 100.0f, min, max);
        }

        public static float MapByPercentsRange(float value, Range range)
        {
            return Map(value, 0.0f, 100.0f, range.Min, range.Max);
        }

        public static float MapToPercents(float value, float min, float max)
        {
            return Map(value, min, max, 0.0f, 100.0f);
        }
    }
}
