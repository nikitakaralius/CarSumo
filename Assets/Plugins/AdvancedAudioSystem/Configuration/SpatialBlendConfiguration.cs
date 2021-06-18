using UnityEngine;

namespace AdvancedAudioSystem.Configuration
{
    public class SpatialBlendConfiguration : IAudioConfiguration
    {
        [SerializeField] private readonly SpatialBlendMode _spatialBlend = SpatialBlendMode.Spatial3D;

        public void ApplyTo(AudioSource audioSource)
        {
            audioSource.spatialBlend = (float)_spatialBlend;
        }

        private enum SpatialBlendMode
        {
            Spatial2D = 0,
            Spatial3D = 1
        }
    }
}
