using AdvancedAudioSystem.Emitters;
using CarSumo;
using CarSumo.GUI.Processees;
using CarSumo.GUI.Processes;
using CarSumo.Structs;
using DG.Tweening;
using UnityEngine.UI;

public static class Configure
{
    public static float Duration => 0.2f;
    
    public static Range Range => new Range(-50, 50);

    public static TweenData TweenData()
    {
        return new TweenData(Range, Duration, Ease.Linear);
    }

    public static VerticalLayoutSpacingTween VerticalLayoutSpacingTween(out VerticalLayoutGroup group, out TweenData data)
    {
        @group = Create.VerticalLayoutGroup();
        data = Configure.TweenData();
        
        return new VerticalLayoutSpacingTween(@group, data);
    }

    public static GUIAudioProcess GUIAudioProcess(out MonoSoundEmitter soundEmitter)
    {
        var audioCue = Create.AudioCue();
        soundEmitter = Create.MonoSoundEmitter();
        return new GUIAudioProcess(soundEmitter, audioCue);
    }
}