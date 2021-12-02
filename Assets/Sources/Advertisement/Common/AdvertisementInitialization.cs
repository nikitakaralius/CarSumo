using UnityEngine;

namespace Advertisement.Common
{
	public class AdvertisementInitialization : MonoBehaviour
	{
		[SerializeField] private AdvertisementPreferences _preferences;

		private void OnEnable()
		{
			IronSource.Agent.init(_preferences.AppKey);
			IronSource.Agent.setMetaData("UnityAds_coppa", "true");
			
#if UNITY_EDITOR
			IronSource.Agent.setAdaptersDebug(true);
#endif
			IronSource.Agent.validateIntegration();
		}
	}
}