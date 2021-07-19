using System.Collections.Generic;
using CarSumo.GUI.Processees;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CarSumo.GUI.Processes
{
    public class ImageTransparencyTween : DOTweenProcess
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

        public override void Init()
        {
            _transparent = _transparentOnStart;
        }

        public override void OnApplied()
        {
            _transparent = !_transparent;
        }

        protected override IEnumerable<Tween> CreateTweenSection()
        {
            float to = _transparent ? 1 : 0;

            Tween[] tweens = new Tween[_images.Length];

            for (var i = 0; i < _images.Length; i++)
            {
                Image image = _images[i];
                Tween tween = DOTween.ToAlpha(() => image.color,color => image.color = color, to, _duration);
                tweens[i] = tween;
            }

            return tweens;
        }
    }
}
