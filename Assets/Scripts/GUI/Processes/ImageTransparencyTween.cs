using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CarSumo.GUI.Processes
{
    public class ImageTransparencyTween : IGUIProcess
    {
        [SerializeField] private Image[] _images = new Image[0];
        [SerializeField] private float _duration;
        [SerializeField] private bool _transparentOnStart;

        private bool _transparent;

        public ImageTransparencyTween(Image[] images, float duration, bool transparentOnStart)
        {
            _images = images;
            _duration = duration;
            _transparentOnStart = transparentOnStart;

            Init();
        }

        public void Init()
        {
            _transparent = _transparentOnStart;
        }

        public void ApplyProcess()
        {
            float to = _transparent ? 1 : 0;

            ChangeTransparency(to, _duration);

            _transparent = !_transparent;
        }

        private void ChangeTransparency(float to, float duration)
        {
            foreach (Image image in _images)
            {
                DOTween.ToAlpha(() => image.color, color => image.color = color, to, duration);
            }
        }
    }
}
