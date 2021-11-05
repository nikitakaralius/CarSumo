using UnityEngine;

namespace CarSumo.DataModel.Accounts
{
    public class Icon
    {
        public readonly Sprite Sprite;
        public readonly string Asset;

        public Icon(Sprite sprite, string asset)
        {
            Sprite = sprite;
            Asset = asset;
        }
    }
}