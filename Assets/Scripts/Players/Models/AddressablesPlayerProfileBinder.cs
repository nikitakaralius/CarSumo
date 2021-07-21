using DataManagement.Players.Models;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CarSumo.Players.Models
{
    public class AddressablesPlayerProfileBinder : IPlayerProfileBinder
    {
        private const string DefaultIcon = "Players/DefaultUserIcon.png";

        public PlayerProfile BindFrom(Player player)
        {
            return new PlayerProfile(player.Name, LoadSpriteByPath(player.Icon ?? DefaultIcon), player.Resources);
        }

        private Sprite LoadSpriteByPath(string path)
        {
            return Addressables.LoadAssetAsync<Sprite>(path).Result;
        }
    }
}