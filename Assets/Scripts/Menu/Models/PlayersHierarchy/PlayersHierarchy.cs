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
        [SerializeField] private Transform _selectedPlayerRoot;
        [SerializeField] private Transform _otherPlayersRoot;

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
            CreateSelectedPlayer(_playersDataService.StoredData);
            CreateOtherProfiles(_playersDataService.StoredData);
        }

        private async void CreateSelectedPlayer(IPlayersRepository repository)
        {
            PlayerProfile profile = _profileBinder.BindFrom(repository.SelectedPlayer);
            await CreateProfile(profile, _selectedPlayerRoot);
        }

        private async void CreateOtherProfiles(IPlayersRepository repository)
        {
            IEnumerable<PlayerProfile> profiles = GetPlayerProfilesWithoutSelected(repository);
            int slots = 0;
            
            foreach (PlayerProfile profile in profiles)
            {
                slots++;
                await CreateProfile(profile, _otherPlayersRoot);
            }

            const int selectedPlayer = 1;
            await FillBlanks(SlotsCount - slots - selectedPlayer);
        }

        private async Task CreateProfile(PlayerProfile profile, Transform root)
        {
            GameObject profileInstance = await _playerViewItemPrefab.InstantiateAsync(root).Task;
            PlayerViewItem component = profileInstance.GetComponent<PlayerViewItem>();
            component.AssignFrom(profile);
        }

        private async Task FillBlanks(int count)
        {
            for (int i = 0; i < count; i++)
            {
                await _blankViewItem.InstantiateAsync(_otherPlayersRoot).Task;
            }
        }

        private IEnumerable<PlayerProfile> GetPlayerProfilesWithoutSelected(IPlayersRepository repository)
        {
            return repository.Players
                .Where(player => player != repository.SelectedPlayer)
                .Select(player => _profileBinder.BindFrom(player));
        }
    }
}