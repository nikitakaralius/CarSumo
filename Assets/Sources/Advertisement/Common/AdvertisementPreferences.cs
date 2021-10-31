using UnityEngine;

namespace Advertisement.Common
{
	[CreateAssetMenu(fileName = "AdvertisementPreferences", menuName = "Advertisement/Preferences")]
	public class AdvertisementPreferences : ScriptableObject
	{
		[SerializeField] private string _appKey;

		public string AppKey => _appKey;
	}
}