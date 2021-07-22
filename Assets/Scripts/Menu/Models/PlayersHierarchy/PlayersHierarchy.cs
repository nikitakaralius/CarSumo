using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarSumo.Players.Models;
using DataManagement.Players.Models;
using DataManagement.Players.Services;
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

        private PlayersDataService _playersDataService;
        private IPlayerProfileBinder _profileBinder;
        
        [Inject]
        private void Construct(PlayersDataService playersDataService, IPlayerProfileBinder profileBinder)
        {
            _playersDataService = playersDataService;
            _profileBinder = profileBinder;
        }
        
        private void Start()
        {
            CreateSelectedPlayerProfile(_playersDataService.StoredData);
            CreateProfiles(_playersDataService.StoredData);
        }

        private async void CreateSelectedPlayerProfile(IPlayersRepository repository)
        {
            PlayerProfile profile = _profileBinder.BindFrom(repository.SelectedPlayer);
            PlayerViewItem viewItem = await CreateViewItem(profile, _hierarchyRoot);
        }

        private async void CreateProfiles(IPlayersRepository repository)
        {
            IEnumerable<PlayerProfile> profiles = BuildPlayerProfilesExceptSelected(repository);
            int slots = repository.Players.Count;

            foreach (PlayerProfile profile in profiles)
            {
                await CreateViewItem(profile, _hierarchyRoot);
            }
            
            await FillBlanks(SlotsCount - slots);
        }

        private async Task<PlayerViewItem> CreateViewItem(PlayerProfile profile, Transform root)
        {
            GameObject profileInstance = await _playerViewItemPrefab.InstantiateAsync(root).Task;
            PlayerViewItem component = profileInstance.GetComponent<PlayerViewItem>();
            component.Init(profile);
            return component;
        }

        private async Task FillBlanks(int count)
        {
            for (int i = 0; i < count; i++)
            {
                await _blankViewItem.InstantiateAsync(_hierarchyRoot).Task;
            }
        }

        private IEnumerable<PlayerProfile> BuildPlayerProfilesExceptSelected(IPlayersRepository repository)
        {
            return repository.Players
                .Where(player => player != repository.SelectedPlayer)
                .Select(player => _profileBinder.BindFrom(player));
        }
    }
}