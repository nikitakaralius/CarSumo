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

        public object Key => ResolveKey(_spriteReference);

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

        private object ResolveKey(AssetReferenceSprite reference)
        {
#if UNITY_EDITOR
            return reference.AssetGUID;
#endif

#pragma warning disable 162
            return reference.RuntimeKey;
#pragma warning restore 162
        }
    }
}