using UnityEngine.UI;

namespace Menu.Extensions
{
	public static class LayoutGroupExtensions
	{
		public static void EnableElementsUpdate(this LayoutGroup layoutGroup)
		{
			layoutGroup.enabled = true;
		}

		public static void DisableElementsUpdate(this LayoutGroup layoutGroup)
		{
			layoutGroup.enabled = false;
		}
	}
}