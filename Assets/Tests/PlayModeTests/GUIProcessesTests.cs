using System.Collections;
using CarSumo;
using CarSumo.GUI;
using CarSumo.GUI.Processes;
using CarSumo.Structs;
using DG.Tweening;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using CarSumo.GUI.Processees;

public class GUIProcessesTests
{
    [UnityTest]
    public IEnumerator ImageTransparencyTween_SetImageVisible_And_InvisibleAfterReuse()
    {
        var image = new GameObject("Image").AddComponent<Image>();

        float duration = 0.5f;

        IGUIProcess process = new ImageTransparencyTween(new[] { image }, duration, false);

        process.ApplyProcess();

        yield return new WaitForSeconds(duration);

        Assert.AreEqual(image.color.a, 0.0f);

        process.ApplyProcess();

        yield return new WaitForSeconds(duration);

        Assert.AreEqual(image.color.a, 1.0f);
    }

    [UnityTest]
    public IEnumerator ImageTransparencyTween_SetImageVisible_And_InvisibleAfterReuse_IfTransparentOnStart()
    {
        var image = new GameObject("Image").AddComponent<Image>();

        float duration = 0.5f;

        IGUIProcess process = new ImageTransparencyTween(new[] {image}, duration, true);

        process.ApplyProcess();

        yield return new WaitForSeconds(duration);

        Assert.AreEqual(image.color.a, 1.0f);

        process.ApplyProcess();

        yield return new WaitForSeconds(duration);

        Assert.AreEqual(image.color.a, 0.0f);
    }
}