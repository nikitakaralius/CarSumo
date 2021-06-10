using UnityEngine;

namespace CarSumo.Utilities
{
    public static class Trigonometry
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