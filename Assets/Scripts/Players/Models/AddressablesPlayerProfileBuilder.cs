using DataManagement.Players.Models;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CarSumo.Players.Models
{
    public class AddressablesPlayerProfileBuilder : IPlayerProfileBuilder
    {
        private const string DefaultIcon = "Players/DefaultUserIcon.png";

        public PlayerProfile BuildFrom(Player player)
        {
            return new PlayerProfile(player.Name, LoadSpriteByPath(player.Icon ?? DefaultIcon), player.Resources);
        }

        private Sprite LoadSpriteByPath(string path)
        {
            return Addressables.LoadAssetAsync<Sprite>(path).Result;
        }
    }
}