using System;
using UnityEngine;

namespace BaseData.CompositeRoot.Common
{
	public class CompositionOrder : MonoBehaviour
	{
		[SerializeField] private CompositionRoot[] _roots = Array.Empty<CompositionRoot>();

		private void OnValidate()
		{
			foreach (CompositionRoot compositionRoot in _roots)
				if (compositionRoot != null)
					compositionRoot.enabled = false;
		}

		private async void OnEnable()
		{
			foreach (CompositionRoot compositionRoot in _roots)
			{
				compositionRoot.enabled = true;
				await compositionRoot.ComposeAsync();
			}
		}
	}
}