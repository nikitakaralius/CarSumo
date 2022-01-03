using UnityEngine;

namespace Sources.Core.Interfaces
{
	public interface IVisible
	{
		void Show();
		void Hide();
	}

	public class Visible : MonoBehaviour, IVisible
	{
		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}
	}
}