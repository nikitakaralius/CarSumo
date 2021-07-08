using AdvancedAudioSystem.Emitters;
using CarSumo;
using CarSumo.GUI.Processees;
using CarSumo.GUI.Processes;
using CarSumo.Structs;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public static class Configure
{
    public static float Duration => 0.2f;

    public static TweenData TweenData()
    {
        return new TweenData(Create.Range(), Duration, Ease.Linear);
    }

    public static VerticalLayoutSpacingTween VerticalLayoutSpacingTween(out VerticalLayoutGroup group, out TweenData data)
    {
        group = Create.VerticalLayoutGroup();
        data = Configure.TweenData();
        
        return new VerticalLayoutSpacingTween(@group, data);
    }

    public static GUIAudioProcess GUIAudioProcess(out MonoSoundEmitter soundEmitter)
    {
        var audioCue = Create.AudioCue();
        soundEmitter = Create.MonoSoundEmitter();
        return new GUIAudioProcess(soundEmitter, audioCue);
    }
    
    public static TweenData<Vector2> Vector2TweenData()
    {
        Range<Vector2> range = Create.Vector2Range();

        return new TweenData<Vector2>(range, Duration, Ease.Linear);
    }

    public static AnchorPositionTween AnchorPositionTween(out TweenData<Vector2> tweenData, out RectTransform rectTransform)
    {
        rectTransform = Create.RectTransform();
        tweenData = Configure.Vector2TweenData();

        return new AnchorPositionTween(new[] {rectTransform}, tweenData);
    }
    
    public static ImageTransparencyTween ImageTransparencyTween(Image image, bool transparentOnStart)
    {
        return new ImageTransparencyTween(new[] {image}, Duration, transparentOnStart);
    }
}