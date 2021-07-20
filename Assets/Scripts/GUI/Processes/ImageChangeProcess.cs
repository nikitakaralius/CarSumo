using UnityEngine;
using UnityEngine.UI;

namespace CarSumo.GUI.Processes
{
    public class ImageChangeProcess : IGUIProcess
    {
        [SerializeField] private Image _targetImage;
        [SerializeField] private Sprite[] _sprites;

        private int _currentImageIndex;
        
        public ImageChangeProcess(Image targetImage, Sprite[] sprites)
        {
            _targetImage = targetImage;
            _sprites = sprites;
        }

        private int NextImageIndex => (int) Mathf.Repeat(_currentImageIndex + 1, _sprites.Length);

        public void Init()
        {
            _targetImage.sprite = _sprites[0];
        }

        public void Apply()
        {
            ChangeOnNextImage();
        }

        public void Stop() { }

        private void ChangeOnNextImage()
        {
            _currentImageIndex = NextImageIndex;
            _targetImage.sprite = _sprites[_currentImageIndex];
        }
    }
}