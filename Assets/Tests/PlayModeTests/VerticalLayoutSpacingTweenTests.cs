using System.Collections;
using System.Collections.Generic;
using CarSumo;
using CarSumo.GUI;
using CarSumo.GUI.Processes;
using CarSumo.Structs;
using DG.Tweening;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class VerticalLayoutSpacingTweenTests
{
    [UnityTest]
    public IEnumerator VerticalLayout_ChangesSpacingToMaxAndReturnsItToMin_AfterAnotherApplyProcess()
    {
        var gameObject = new GameObject("VerticalLayoutSpacingTweenTests");
        VerticalLayoutGroup group = gameObject.AddComponent<VerticalLayoutGroup>();

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
}
