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
using UnityEditor;
using AdvancedAudioSystem;
using CarSumo.GUI.Processees;
using AdvancedAudioSystem.Emitters;

public class GUIProcessesTests
{
    [UnityTest]
    public IEnumerator GUIAudioProcess_PlaysAudioCueOnSoundEmitter()
    {
        var assetPath = "Explosion Cue Test";

        var audioCue = Resources.Load<AudioCue>(assetPath);

        var soundEmitter = new GameObject("Sound Emitter").AddComponent<MonoSoundEmitter>();
        var audioListener = new GameObject("Audio Listener").AddComponent<AudioListener>();

        IGUIProcess process = new GUIAudioProcess(soundEmitter, audioCue);

        process.ApplyProcess();

        yield return new WaitForSeconds(0.1f);

        Assert.IsTrue(soundEmitter.AudioSourceProperty.IsPlaying);
    }

    [UnityTest]
    public IEnumerator AnchorPositionTween_ChangesPositionToMaxAndReturnsItToMin_AfterReuse()
    {
        var rectTransform = new GameObject("Rect transform").AddComponent<RectTransform>();
        var range = new Range<Vector2>(Vector2.one * -10, Vector2.one * 10);
        var duration = 0.5f;
        var data = new TweenData<Vector2>(range, duration, Ease.Unset);

        IGUIProcess process = new AnchorPositionTween(new[] { rectTransform }, data);

        process.ApplyProcess();

        yield return new WaitForSeconds(duration);

        AreVectorsEqual(rectTransform.anchoredPosition, data.Range.Max);

        process.ApplyProcess();

        yield return new WaitForSeconds(duration);

        AreVectorsEqual(rectTransform.anchoredPosition, data.Range.Min);
    }

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

        IGUIProcess process = new ImageTransparencyTween(new[] { image }, duration, true);

        process.ApplyProcess();

        yield return new WaitForSeconds(duration);

        Assert.AreEqual(image.color.a, 1.0f);

        process.ApplyProcess();

        yield return new WaitForSeconds(duration);

        Assert.AreEqual(image.color.a, 0.0f);
    }

    private void AreVectorsEqual(Vector2 a, Vector2 b)
    {
        Assert.AreEqual(a.x, b.x, Vector2.kEpsilon);
        Assert.AreEqual(a.y, b.y, Vector2.kEpsilon);
    }
}