using AdvancedAudioSystem;
using CarSumo.DataModel.Accounts;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace Menu.Accounts
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class IconView : MonoBehaviour
    {
        [SerializeField] private AssetReferenceSprite _spriteReference;

        private Image _image;
        private IAccountIconReceiver _accountReceiver;
        private IAudioPlayer _audioPlayer;

        [Inject]
        private void Construct(IAccountIconReceiver accountIconReceiver, IAudioPlayer player)
        {
            _accountReceiver = accountIconReceiver;
            _audioPlayer = player;
        }

        private async void Awake()
        {
            Sprite sprite = await _spriteReference.LoadAssetAsync<Sprite>().Task;
            
            _image = GetComponent<Image>();
            Button button = GetComponent<Button>();
            
            Validate(sprite);

            button.onClick.AddListener(() => ReceiveIcon(sprite, _spriteReference));
            button.onClick.AddListener(_audioPlayer.Play);
        }

        private void ReceiveIcon(Sprite sprite, AssetReferenceSprite reference)
        {
            Icon icon = new Icon(sprite, ResolveKey(reference));
            _accountReceiver.ReceiveIcon(icon);
        }
        
        private void Validate(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        private string ResolveKey(AssetReferenceSprite reference)
        {
#if UNITY_EDITOR
            return reference.AssetGUID;
#endif

#pragma warning disable 162
            return reference.RuntimeKey.ToString();
#pragma warning restore 162
        }
    }
}