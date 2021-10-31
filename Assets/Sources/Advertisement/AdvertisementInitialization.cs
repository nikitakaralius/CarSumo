using UnityEngine;

namespace Advertisement
{
	public class AdvertisementInitialization : MonoBehaviour
	{
		[SerializeField] private AdvertisementPreferences _preferences;
		
		private void OnEnable()
		{
			IronSource.Agent.init(_preferences.AppKey);
			IronSource.Agent.validateIntegration();
		}
	}
}