using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace CustomEditors.Tools
{
	public class VehiclesWindow : OdinMenuEditorWindow
	{
		private const string VehicleStatsPath = "Assets/Bundles/Vehicles/ScriptableObjects/Stats";
		private const string AssetProviders = "Assets/Bundles/Vehicles/ScriptableObjects/AssetProviders";
		private const string Audio = "Assets/Bundles/Vehicles/ScriptableObjects/Audio";
		private const string PhysicMaterials = "Assets/Bundles/Vehicles/PhysicsMaterials";

		[MenuItem("Tools/Vehicles")]
		public static void OpenWindow() => GetWindow<VehiclesWindow>().Show();

		protected override OdinMenuTree BuildMenuTree()
		{
			var tree = new OdinMenuTree();

			tree
				.AddAllAssetsAtPath("Stats", VehicleStatsPath, typeof(ScriptableObject))
				.AddIcons(EditorIcons.Car);
			
			tree
				.AddAllAssetsAtPath("Physic Materials", PhysicMaterials, typeof(PhysicMaterial))
				.AddThumbnailIcons(true);

			tree
				.AddAllAssetsAtPath("Asset Providers", AssetProviders, typeof(ScriptableObject))
				.AddIcons(EditorIcons.FileCabinet);

			tree
				.AddAllAssetsAtPath("Audio", Audio, typeof(ScriptableObject), true)
				.AddIcons(EditorIcons.Sound);

			return tree;
		}
	}
}