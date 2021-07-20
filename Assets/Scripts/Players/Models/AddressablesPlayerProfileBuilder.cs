using DataManagement.Players.Models;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CarSumo.Players.Models
{
    public class AddressablesPlayerProfileBuilder : IPlayerProfileBuilder
    {
        public PlayerProfile BuildFrom(Player player)
        {
            return new PlayerProfile(player.Name, LoadSpriteByPath(player.Icon), player.Resources);
        }

        private Sprite LoadSpriteByPath(string path)
        {
            return Addressables.LoadAssetAsync<Sprite>(path).Result;
        }
    }
}