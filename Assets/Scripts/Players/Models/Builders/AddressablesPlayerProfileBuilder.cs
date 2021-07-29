using System.Threading.Tasks;
using DataManagement.Players.Models;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CarSumo.Players.Models
{
    public class AddressablesPlayerProfileBuilder : IPlayerProfileBuilder
    {
        private const string DefaultIcon = "Players/Icons/DefaultUserIcon.png";

        public async Task<PlayerProfile> BuildFrom(Player player)
        {
            Sprite icon = await LoadSpriteByKey(player.Icon ?? DefaultIcon);
            return new PlayerProfile(player.Name, icon, player.Resources);
        }

        private async Task<Sprite> LoadSpriteByKey(object key)
        {
            return await Addressables.LoadAssetAsync<Sprite>(key).Task;
        }
    }
}