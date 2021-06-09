using CarSumo.Factory;
using TMPro;
using UnityEngine;

namespace CarSumo.VFX
{
    [CreateAssetMenu(fileName = "Text3D Factory", menuName = "CarSumo/VFX/Other/Text3DFactory")]
    public class Text3DFactory : FactoryScriptableObject<TMP_Text>
    {
        [SerializeField] private TMP_Text _textPrefab;

        public override TMP_Text Create(Transform parent = null)
        {
            return Instantiate(_textPrefab, parent);
        }
    }
}