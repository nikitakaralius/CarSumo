#if UNITY_EDITOR

using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;


namespace CarSumo.SceneManagement
{
    public class ScenePostprocessor : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            DetectSceneImport(importedAssets);
            DetectSceneDeletion(deletedAssets);
            DetectSceneMovement(movedAssets, movedFromAssetPaths);
        }

        private static void DetectSceneImport(string[] importedAssets)
        {
            foreach (var assetPath in importedAssets)
            {
                if (IsSceneAssetAdded(assetPath) == false)
                    continue;

                var name = Path.GetFileNameWithoutExtension(assetPath);
                var guid = AssetDatabase.AssetPathToGUID(assetPath);
                var sourceCode = TypedSceneGenerator.Generate(name, name, guid);

                if (ContainsSceneWithGUID(guid) == false)
                {
                    var newSceneList = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes)
                    {
                        new EditorBuildSettingsScene(assetPath, true)
                    };
                    EditorBuildSettings.scenes = newSceneList.ToArray();
                }

                TypedSceneStorage.Save(name, sourceCode);
            }
        }

        private static void DetectSceneDeletion(string[] deletedAssets)
        {
            foreach (var assetPath in deletedAssets)
            {
                if (IsSceneAssetDeleted(assetPath) == false)
                    continue;

                var newSceneList = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
                newSceneList.RemoveAll(scene => scene.path == assetPath);
                EditorBuildSettings.scenes = newSceneList.ToArray();
                TypedSceneStorage.Delete(Path.GetFileNameWithoutExtension(assetPath));
            }
        }

        private static void DetectSceneMovement(string[] movedAssets, string[] movedFromAssetPaths)
        {
            for (int i = 0; i < movedAssets.Length; i++)
            {
                if (Path.GetExtension(movedFromAssetPaths[i]) != TypedSceneSettings.SceneExtension)
                    continue;

                var oldName = Path.GetFileNameWithoutExtension(movedFromAssetPaths[i]);
                var newName = Path.GetFileNameWithoutExtension(movedAssets[i]);

                if (oldName != newName)
                    TypedSceneStorage.Delete(oldName);
            }
        }

        private static bool ContainsSceneWithGUID(string guid)
        {
            return EditorBuildSettings.scenes.Any(scene => scene.guid.ToString() == guid);
        }

        private static bool IsSceneAssetAdded(string assetPath)
        {
            return Path.GetExtension(assetPath) == TypedSceneSettings.SceneExtension
                   && TypedSceneValidator.ValidateSceneImport(assetPath);
        }

        private static bool IsSceneAssetDeleted(string assetPath)
        {
            return Path.GetExtension(assetPath) == TypedSceneSettings.SceneExtension
                   & TypedSceneValidator.ValidateSceneDeletion(Path.GetFileNameWithoutExtension(assetPath)) == false;
        }
    }
}

#endif