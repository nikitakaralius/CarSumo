using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarSumo.Players.Models;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace CarSumo.Menu.Models
{
    public class PlayersHierarchy : MonoBehaviour
    {
        [SerializeField] private Transform _hierarchyRoot;

        [SerializeField] private AssetReferenceGameObject _playerViewItemPrefab;
        [SerializeField] private AssetReferenceGameObject _blankViewItem;
        
        private const int SlotsCount = 4;

        private IPlayerViewSelect _playerSelect;
        private IPlayerProfilesProvider _profilesProvider;

        [Inject]
        private void Construct(IPlayerViewSelect playerSelect, IPlayerProfilesProvider profilesProvider)
        {
            _playerSelect = playerSelect;
            _profilesProvider = profilesProvider;
        }
        
        private void Start()
        {
            CreateSelectedPlayerProfile(_profilesProvider);
            CreateProfiles(_profilesProvider);
        }

        private async void CreateSelectedPlayerProfile(IPlayerProfilesProvider provider)
        {
            PlayerProfile profile = provider.SelectedPlayer;
            PlayerViewItem viewItem = await CreateViewItem(profile, _hierarchyRoot);
            viewItem.MakeSelected();
        }

        private async void CreateProfiles(IPlayerProfilesProvider provider)
        {
            IEnumerable<PlayerProfile> profiles = provider.OtherPlayers;
            int slots = profiles.Count();

            foreach (PlayerProfile profile in profiles)
            {
                await CreateViewItem(profile, _hierarchyRoot);
            }

            const int selectedPlayer = 1;
            await FillBlanks(SlotsCount - slots - selectedPlayer);
        }

        private async Task<PlayerViewItem> CreateViewItem(PlayerProfile profile, Transform root)
        {
            GameObject profileInstance = await _playerViewItemPrefab.InstantiateAsync(root).Task;
            PlayerViewItem component = profileInstance.GetComponent<PlayerViewItem>();
            component.Init(profile, _playerSelect);
            return component;
        }

        private async Task FillBlanks(int count)
        {
            for (int i = 0; i < count; i++)
            {
                await _blankViewItem.InstantiateAsync(_hierarchyRoot).Task;
            }
        }
    }
}