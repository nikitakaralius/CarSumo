using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarSumo.Infrastructure.Services.Instantiate;
using CarSumo.Players.Models;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace CarSumo.Menu.Models
{
    public class PlayersHierarchy : MonoBehaviour
    {
        [SerializeField] private Transform _hierarchyRoot;

        [SerializeField] private AssetReferenceGameObject _playerViewItemPrefab;
        [SerializeField] private AssetReferenceGameObject _blankViewItemPrefab;
        
        private const int SlotsCount = 4;

        private IInstantiateService _instantiateService;
        private IPlayerProfilesProvider _profilesProvider;

        [Inject]
        private void Construct(IInstantiateService instantiateService, IPlayerProfilesProvider profilesProvider)
        {
            _instantiateService = instantiateService;
            _profilesProvider = profilesProvider;
        }
        
        private async void Start()
        {
            await CreateSelectedPlayerProfile(_profilesProvider);
            await CreateProfiles(_profilesProvider);
        }

        private async Task CreateSelectedPlayerProfile(IPlayerProfilesProvider provider)
        {
            PlayerProfile profile = provider.SelectedPlayer;
            PlayerViewItem viewItem = await CreateViewItem(profile, _hierarchyRoot);
            viewItem.MakeSelected();
        }

        private async Task CreateProfiles(IPlayerProfilesProvider provider)
        {
            IEnumerable<PlayerProfile> profiles = provider.OtherPlayers;
            int slots = profiles.Count();

            foreach (PlayerProfile profile in profiles)
            {
                await CreateViewItem(profile, _hierarchyRoot);
            }

            const int selectedPlayer = 1;
            int slotsCount = SlotsCount - slots - selectedPlayer;
            await FillBlanks(slotsCount, _hierarchyRoot);
        }

        private async Task<PlayerViewItem> CreateViewItem(PlayerProfile profile, Transform root)
        {
            PlayerViewItem viewItem = await InstantiatePlayerViewItem(root);
            viewItem.Init(profile);
            return viewItem;
        }

        private async Task<PlayerViewItem> InstantiatePlayerViewItem(Transform root)
        {
            return await _instantiateService.InstantiateAsync<PlayerViewItem>(_playerViewItemPrefab, root);
        }

        private async Task FillBlanks(int count, Transform root)
        {
            for (int i = 0; i < count; i++)
            {
                await _instantiateService.InstantiateAsync<Button>(_blankViewItemPrefab, root);
            }
        }
    }
}