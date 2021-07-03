namespace CarSumo.Calculations
{
    public static class Map
    {
        public static float MapByRanges(float value, float originMin, float originMax, float newMin, float newMax)
        {
            return (value - originMin) * (newMax - newMin) / (originMax - originMin) + newMin;
        }

        public static float MapFromPercentRange(float value, float min, float max)
        {
            return MapByRanges(value, 0.0f, 100.0f, min, max);
        }

        public static float MapFromPercentRange(float value, Range range)
        {
            return MapByRanges(value, 0.0f, 100.0f, range.Min, range.Max);
        }

        public static float MapToPercentsRange(float value, float min, float max)
        {
            return MapByRanges(value, min, max, 0.0f, 100.0f);
        }
    }
}
