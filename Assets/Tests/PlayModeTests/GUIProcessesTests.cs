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
    public IEnumerator VerticalLayoutSpacingTween_ChangesSpacingToMaxAndReturnsItToMin_AfterAnotherApplyProcess()
    {
        VerticalLayoutGroup group = new GameObject("VerticalLayoutSpacingTweenTests").AddComponent<VerticalLayoutGroup>();
        var duration = 0.5f;
        var range = new Range(-50, 50);
        var tweenData = new TweenData(range, 0.5f, Ease.Unset);

        IGUIProcess process = new VerticalLayoutSpacingTween(group, tweenData);

        process.ApplyProcess();

        yield return new WaitForSeconds(duration);

        Assert.AreEqual(tweenData.Range.Max, group.spacing);

        process.ApplyProcess();

        yield return new WaitForSeconds(duration);

        Assert.AreEqual(tweenData.Range.Min, group.spacing);
    }

    [UnityTest]
    public IEnumerator GUIAudioProcess_PlaysAudioCueOnSoundEmitter()
    {
        var assetPath = "Assets/Tests/Resources/Explosion Cue Test.asset";

        var audioCue = (AudioCue) AssetDatabase.LoadAssetAtPath(assetPath, typeof(AudioCue));

        var soundEmitter = new GameObject("Sound Emitter").AddComponent<MonoSoundEmitter>();

        IGUIProcess process = new GUIAudioProcess(soundEmitter, audioCue);

        process.ApplyProcess();

        yield return new WaitForSeconds(0.1f);

        Assert.IsTrue(soundEmitter.AudioSource.isPlaying);
    }
}
