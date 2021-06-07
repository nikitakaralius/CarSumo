#if UNITY_EDITOR

using System.IO;
using System.Text;
using UnityEditor;


namespace CarSumo.SceneManagement
{
    public class TypedSceneValidator
    {
        public static bool ValidateSceneImport(string scenePath)
        {
            var name = Path.GetFileNameWithoutExtension(scenePath);
            var validName = GetValidName(name);

            if (name != validName)
            {
                var validPath = $@"{Path.GetDirectoryName(scenePath)}\{validName}{TypedSceneSettings.SceneExtension}";
                File.Move(scenePath, validPath);
                File.Delete(scenePath + TypedSceneSettings.MetaExtension);

                AssetDatabase.ImportAsset(validPath, ImportAssetOptions.ForceUpdate);
                return false;
            }

            return SceneAnalyzer.TryAddTypedProcessor(AssetDatabase.AssetPathToGUID(scenePath)) == false;
        }

        public static bool ValidateSceneDeletion(string sceneName)
        {
            var assets = AssetDatabase.FindAssets(sceneName);

            foreach (var asset in assets)
            {
                var path = AssetDatabase.GUIDToAssetPath(asset);
                var name = Path.GetFileNameWithoutExtension(path);

                if (name != sceneName)
                    continue;

                if (Path.GetExtension(path) == TypedSceneSettings.SceneExtension)
                    return true;
            }

            return false;
        }

        private static string GetValidName(string sceneName)
        {
            var stringBuilder = new StringBuilder();

            if (char.IsLetter(sceneName[0]) == false && sceneName[0] != '_')
                stringBuilder.Append('_');

            foreach (var symbol in sceneName)
            {
                bool valid = char.IsLetterOrDigit(symbol) || symbol == '_';
                stringBuilder.Append(valid ? symbol : '_');
            }

            return stringBuilder.ToString();
        }
    }
}

#endif