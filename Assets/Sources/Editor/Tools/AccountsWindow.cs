using System;
using System.Collections.Generic;
using CarSumo.DataModel.Accounts;
using CarSumo.Teams;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace CustomEditors.Tools
{
	public class AccountsWindow : EditorWindow
	{
		[MenuItem("Tools/Accounts")]
		public static AccountsWindow FocusOrCreateWindow() => GetWindow<AccountsWindow>("Accounts");

		private Team _team;
		
		private DiContainer GlobalContainer => ProjectContext.Instance.Container;

		private void OnGUI()
		{
			DisplayVehicleLayout();
		}

		private void DisplayVehicleLayout()
		{
			using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
			{
				bool showVehicleLayoutClicked = GUILayout.Button("Show vehicle layout");

				IEnumerable<Account> accounts = Array.Empty<Account>();

				if (showVehicleLayoutClicked)
				{
					var repository = GlobalContainer.TryResolve<IAccountRepository>();
				}
			}
		}
	}
}