#if UNITY_EDITOR

using System.IO;
using UnityEditor;

namespace CarSumo.SceneManagement
{
    public static class TypedSceneStorage
    {
        public static void Save(string className, string sourceCode)
        {
            var path = $"{TypedSceneSettings.SaveDirectory}{className}{TypedSceneSettings.ClassExtension}";
            Directory.CreateDirectory(TypedSceneSettings.SaveDirectory);

            if (File.Exists(path) && File.ReadAllText(path) == sourceCode)
                return;

            File.WriteAllText(path, sourceCode);
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }

        public static void Delete(string className)
        {
            var path = $"{TypedSceneSettings.SaveDirectory}{className}{TypedSceneSettings.ClassExtension}";

            if (File.Exists(path) == false)
                return;

            AssetDatabase.DeleteAsset(path);
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }
    }
}

#endif