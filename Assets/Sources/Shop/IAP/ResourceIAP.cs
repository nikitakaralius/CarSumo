using CarSumo.DataModel.GameResources;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

namespace Shop.IAP
{
	public class ResourceIAP : MonoBehaviour
	{
		[Header("Preferences")]
		[SerializeField] private ResourceId _resource;
		[SerializeField, Min(0)] private int _amount;

		[Header("View")] 
		[SerializeField] private TextMeshProUGUI _amountText;

		private IClientResourceOperations _resourceOperations;

		[Inject]
		private void Construct(IClientResourceOperations resourceOperations)
		{
			_resourceOperations = resourceOperations;
		}

		private void OnValidate()
		{
			_amountText.text = _amount.ToString();
		}

		public void OnPurchaseCompleted(Product product)
		{
			_resourceOperations.Receive(_resource, _amount);
		}
	}
}