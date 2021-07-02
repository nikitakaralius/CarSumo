#if UNITY_EDITOR

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace CarSumo.SceneManagement
{
    public static class SceneAnalyzer
    {
        public static IEnumerable<Type> GetLoadingParameters(string sceneGUID)
        {
            var loadParameters = new HashSet<Type> {null};

            TryAnalyzeScene(sceneGUID, scene =>
            {
                var componentTypes = GetAllTypes(scene);

                foreach (var type in componentTypes)
                {
                    if (!ImplementsISceneLoadHandler(type))
                        continue;

                    var loadMethods = type.GetMethods()
                        .Where(method => method.Name == "OnSceneLoaded");

                    foreach (var method in loadMethods) 
                        loadParameters.Add(method.GetParameters()[0].ParameterType);
                }
            });

            if (loadParameters.Count > 1)
                loadParameters.Remove(null);

            return loadParameters;
        }

        public static bool TryAddTypedProcessor(string sceneGUID)
        {
            bool added = false;

            TryAnalyzeScene(sceneGUID, scene =>
            {
                var componentTypes = GetAllTypes(scene);

                if (componentTypes.Contains(typeof(TypedProcessor)) == false)
                {
                    var gameObject = new GameObject("TypedProcessor");
                    gameObject.AddComponent<TypedProcessor>();

                    Undo.RegisterCreatedObjectUndo(gameObject, "Added Typed Processor");
                    EditorSceneManager.SaveScene(scene);

                    added = true;
                }
            });

            return added;
        }

        private static bool ImplementsISceneLoadHandler(Type type)
        {
            return type.GetInterfaces().Any(x =>
                x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ISceneLoadHandler<>));
        }

        private static void TryAnalyzeScene(string sceneGUID, Action<Scene> analyzer)
        {
            var scene = SceneManager.GetActiveScene();
            var scenePath = scene.path;
            var targetPath = AssetDatabase.GUIDToAssetPath(sceneGUID);

            if (targetPath == scenePath)
            {
                analyzer?.Invoke(scene);
                return;
            }

            if (File.Exists(targetPath) == false)
                return;

            scene = EditorSceneManager.OpenScene(targetPath, OpenSceneMode.Additive);
            SceneManager.SetActiveScene(scene);
            analyzer?.Invoke(scene);
            EditorSceneManager.CloseScene(scene, true);
        }

        private static IEnumerable<Component> GetAllComponents(Scene activeScene)
        {
            var rootObjects = activeScene.GetRootGameObjects();
            var components = new List<Component>(rootObjects.Length);

            foreach (var gameObject in rootObjects)
                components.AddRange(gameObject.GetComponentsInChildren<Component>());

            return components;
        }

        private static IEnumerable<Type> GetAllTypes(Scene activeScene)
        {
            var components = GetAllComponents(activeScene);
            var types = new HashSet<Type>();

            foreach (var component in components)
                types.Add(component.GetType());

            return types;
        }
    }
}

#endif