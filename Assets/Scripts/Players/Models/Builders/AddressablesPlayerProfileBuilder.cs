using DataManagement.Players.Models;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CarSumo.Players.Models
{
    public class AddressablesPlayerProfileBuilder : IPlayerProfileBuilder
    {
        private const string DefaultIcon = "Players/Icons/DefaultUserIcon.png";

        public PlayerProfile BuildFrom(Player player)
        {
            return new PlayerProfile(player.Name, LoadSpriteByKey(player.Icon ?? DefaultIcon), player.Resources);
        }

        private Sprite LoadSpriteByKey(object key)
        {
            return Addressables.LoadAssetAsync<Sprite>(key).Result;
        }
    }
}