using CarSumo.DataModel.Accounts;
using GuiBaseData.Accounts;
using UniRx;
using Zenject;

namespace Menu.Accounts
{
	public class ActiveAccountView : AccountView
	{
		private IAccountStorage _accountStorage;
		private IAccountStorageMessages _storageMessages;
		private readonly CompositeDisposable _subscriptions = new CompositeDisposable();

		[Inject]
		private void Construct(IAccountStorage accountStorage, IAccountStorageMessages storageMessages)
		{
			_accountStorage = accountStorage;
			_storageMessages = storageMessages;
		}

		private void Awake()
		{
			_accountStorage.ActiveAccount
				.Subscribe(ChangeAccount)
				.AddTo(_subscriptions);

			_storageMessages
				.ObserveAnyAccountValueChanged()
				.Subscribe(ChangeAccount)
				.AddTo(_subscriptions);
		}

		private void OnDestroy()
		{
			_subscriptions.Dispose();
		}
	}
}