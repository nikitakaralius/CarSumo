using UnityEngine;

namespace CarSumo.Calculations
{
    public static class HarmonicMotion
    {
        public static float CosHarmonicMotion(float x, float amplitude, float frequency, float startPhase)
        {
            return amplitude * Mathf.Cos(frequency * x + startPhase);
        }
        public static float SinHarmonicMotion(float x, float amplitude, float frequency, float startPhase)
        {
            return amplitude * Mathf.Sin(frequency * x + startPhase);
        }
    }
}