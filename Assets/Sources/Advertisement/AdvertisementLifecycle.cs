using UnityEngine;

namespace Advertisement
{
	public class AdvertisementLifecycle : MonoBehaviour
	{
		private void OnEnable()
		{
			DontDestroyOnLoad(gameObject);
		}

		private void OnApplicationPause(bool pauseStatus)
		{
			IronSource.Agent.onApplicationPause(pauseStatus);
		}
	}
}