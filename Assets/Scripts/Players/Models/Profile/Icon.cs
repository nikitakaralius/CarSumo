using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace CarSumo.Players.Models
{
    [RequireComponent(typeof(Image))]
    public class Icon : MonoBehaviour
    {
        [SerializeField] private AssetReferenceSprite _spriteReference;

        private Sprite _sprite;
        private Image _image;

        public object Key => _spriteReference.RuntimeKey;

        private async void Awake()
        {
            _sprite = await _spriteReference.LoadAssetAsync<Sprite>().Task;
            _image = GetComponent<Image>();
            Validate(_sprite);
        }

        public void DrawOn(Icon otherIcon)
        {
            otherIcon._spriteReference = _spriteReference;
            otherIcon.Validate(_sprite);
        }

        private void Validate(Sprite sprite)
        {
            _image.sprite = sprite;
        }
    }
}