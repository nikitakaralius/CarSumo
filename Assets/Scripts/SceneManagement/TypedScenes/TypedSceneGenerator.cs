#if UNITY_EDITOR

using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using UnityEngine.SceneManagement;

namespace CarSumo.SceneManagement
{
    public static class TypedSceneGenerator
    {
        public static string Generate(string className, string sceneName, string sceneGUID)
        {
            var targetUnit = new CodeCompileUnit();
            var targetNamespace = new CodeNamespace(TypedSceneSettings.Namespace);
            var targetClass = new CodeTypeDeclaration(className);

            targetNamespace.Imports.Add(new CodeNamespaceImport("UnityEngine.SceneManagement"));
            targetClass.BaseTypes.Add(TypedSceneSettings.BaseClass);
            targetClass.TypeAttributes = TypeAttributes.Class | TypeAttributes.Public;

            AddConstantValue(targetClass, typeof(string), "_sceneName", sceneName);

            var loadingParameters = SceneAnalyzer.GetLoadingParameters(sceneGUID);

            foreach (var loadingParameter in loadingParameters)
                AddLoadingMethod(targetClass, loadingParameter);

            targetNamespace.Types.Add(targetClass);
            targetUnit.Namespaces.Add(targetNamespace);

            var provider = CodeDomProvider.CreateProvider("CSharp");
            var options = new CodeGeneratorOptions {BracingStyle = "C"};
            var code = new StringWriter();

            provider.GenerateCodeFromCompileUnit(targetUnit, code, options);

            return code.ToString();
        }

        private static void AddConstantValue(CodeTypeDeclaration targetClass, Type type, string name, string value)
        {
            var pathConstant = new CodeMemberField(type, name)
            {
                Attributes = MemberAttributes.Private | MemberAttributes.Const,
                InitExpression = new CodePrimitiveExpression(value)
            };

            targetClass.Members.Add(pathConstant);
        }

        private static void AddLoadingMethod(CodeTypeDeclaration targetClass, Type parameterType = null)
        {
            var loadMethod = new CodeMemberMethod()
            {
                Name = "Load",
                Attributes = MemberAttributes.Public | MemberAttributes.Static
            };

            var loadingStatement = "LoadScene(_sceneName, loadSceneMode)";

            if (parameterType != null)
            {
                var parameter = new CodeParameterDeclarationExpression(parameterType, "argument");
                loadMethod.Parameters.Add(parameter);
                loadingStatement = "LoadScene(_sceneName, loadSceneMode, argument)";
            }

            var loadingModeParameter =
                new CodeParameterDeclarationExpression(nameof(LoadSceneMode),
                    "loadSceneMode = LoadSceneMode.Single");

            loadMethod.Parameters.Add(loadingModeParameter);

            loadMethod.Statements.Add(new CodeSnippetExpression(loadingStatement));
            targetClass.Members.Add(loadMethod);
        }
    }
}

#endif
