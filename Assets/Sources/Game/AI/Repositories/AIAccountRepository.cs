using System;
using System.Collections.Generic;
using System.Linq;
using CarSumo.DataModel.Accounts;
using DataModel.GameData.Vehicles;
using DataModel.Vehicles;
using UnityEngine;

namespace AI.Repositories
{
	[CreateAssetMenu(fileName = "AIAccountRepository", menuName = "AI/Repository")]
	public class AIAccountRepository : ScriptableObject, IAccountRepository
	{
		[Serializable]
		private class UnityAccount
		{
			public string Name;
			public Sprite Icon;
			public VehicleId[] Layout;

			public Account ToAccount() => 
				new Account(
					Name,
					new Icon(Icon, string.Empty),
					new BoundedVehicleLayout(3, Layout));
		}

		[SerializeField] private UnityAccount[] _accounts = Array.Empty<UnityAccount>();
		
		public IEnumerable<Account> Accounts => _accounts.Select(x => x.ToAccount());
	}
}