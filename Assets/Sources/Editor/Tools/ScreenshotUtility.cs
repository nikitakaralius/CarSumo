using System;
using System.IO;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace CustomEditors.Tools
{
	public class ScreenshotUtility : OdinEditorWindow
	{
		[MenuItem("Tools/Screenshot Utility")]
		public static void OpenWindow() => GetWindow<ScreenshotUtility>();

		[FolderPath]
		public string SaveDirectory;

		[Button]
		public void TakeScreenshot()
		{
			if (string.IsNullOrEmpty(SaveDirectory))
				throw new InvalidOperationException("Choose save directory");
			
			var timestamp = DateTime.Now;
			var stampString = $"_{timestamp.Year}-{timestamp.Month:00}-{timestamp.Day:00}_{timestamp.Hour:00}-{timestamp.Minute:00}-{timestamp.Second:00}";

			string path = Path.Combine(SaveDirectory, "Screenshot" + stampString + ".png");
			ScreenCapture.CaptureScreenshot(path);
			AssetDatabase.Refresh();

			Debug.Log("New Screenshot taken");
		}
		
		[MenuItem("Tools/Take Screenshot %&x")]
		public static void TakeScreenshotShortcut()
		{
			GetWindow<ScreenshotUtility>().TakeScreenshot();
		}
	}
}