using UnityEngine;
using UnityEngine.UI;

namespace CarSumo.Menu.Models
{
    public class SelectPlayerHighlight : MonoBehaviour
    {
        [SerializeField] private Image _image;
        
        [Header("Graphics")]
        [SerializeField] private Sprite _selected;
        [SerializeField] private Sprite _regular;

        public void MakeSelected()
        {
            _image.sprite = _selected;
        }

        public void MakeRegular()
        {
            _image.sprite = _regular;
        }
    }
}