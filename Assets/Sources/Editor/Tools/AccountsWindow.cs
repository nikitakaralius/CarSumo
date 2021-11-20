using System.Collections.Generic;
using CarSumo.DataModel.Accounts;
using DataModel.Vehicles;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace CustomEditors.Tools
{
	public class AccountsWindow : EditorWindow
	{
		[MenuItem("Tools/Accounts")]
		public static AccountsWindow FocusOrCreateWindow() => GetWindow<AccountsWindow>("Accounts");
		
		private readonly List<Account> _accountsToRender = new List<Account>();

		private DiContainer GlobalContainer => ProjectContext.Instance.Container;

		private void OnGUI()
		{
			using (new GUILayout.VerticalScope(EditorStyles.helpBox))
			{
				UpdateAccountList();
				RenderAccountList();
			}
		}

		private void OnDisable() => SaveChanges();

		private void UpdateAccountList()
		{
			bool showVehicleLayoutClicked = GUILayout.Button("Show All Accounts");

			if (showVehicleLayoutClicked)
			{
				if (EditorApplication.isPlaying == false)
				{
					Debug.Log("You should enter play mode to use this tool");
					return;
				}
				
				var repository = GlobalContainer.TryResolve<IAccountStorage>();

				if (repository.AllAccounts != null)
				{
					_accountsToRender.Clear();
					_accountsToRender.AddRange(repository.AllAccounts);
				}
			}
		}

		private void RenderAccountList()
		{
			foreach (var account in _accountsToRender)
			{
				using (new GUILayout.HorizontalScope())
				{
					GUILayout.Box(new GUIContent(account.Icon.Value.Sprite.texture), new GUIStyle()
					{
						fixedHeight = 100,
						fixedWidth = 100
					});

					using (new GUILayout.VerticalScope(EditorStyles.helpBox))
					{
						GUILayout.Label(account.Name.Value, new GUIStyle(EditorStyles.helpBox)
						{
							fontSize = 24,
							fontStyle = FontStyle.Bold
						});

						foreach (VehicleId vehicle in account.VehicleLayout.ActiveVehicles)
						{
							GUILayout.Label(vehicle.ToString());
						}
					}
				}
				GUILayout.Space(20);
			}
		}
	}
}