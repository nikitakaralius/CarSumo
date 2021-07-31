using UnityEngine;

namespace CarSumo.DataModel.Accounts
{
    public class Icon
    {
        public Sprite Sprite { get; set; }
        public string Asset { get; set; }

        public Icon(Sprite sprite, string asset)
        {
            Sprite = sprite;
            Asset = asset;
        }
    }
}