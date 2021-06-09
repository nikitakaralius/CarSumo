using TMPro;
using UnityEngine;
using CarSumo.Factory;

namespace CarSumo.VFX
{
    [CreateAssetMenu(fileName = "Text3D Emitter", menuName = "CarSumo/VFX/Other/Text3DEmitter")]
    public class Text3DEmitter : EmitterScriptableObject
    {
        [SerializeField] private Text3DFactory _factory;

        private TMP_Text _instance;

        public override void Emit(Transform parent = null)
        {
            _instance = _factory.Create(parent);
        }

        public void SetText(string value)
        {
            _instance.text = value;
        }

        public override void Stop()
        {
            Destroy(_instance.gameObject);
        }
    }
}