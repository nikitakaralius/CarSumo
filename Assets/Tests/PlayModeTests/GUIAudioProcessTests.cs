using System.Collections;
using CarSumo.GUI;
using FluentAssertions;
using UnityEngine;
using UnityEngine.TestTools;

public class GUIAudioProcessTests
{
    [UnityTest]
    public IEnumerator WhenApplyProcess_AndWaitADelay_ThenSoundEmitterShouldPlaying()
    {
        IGUIProcess process = Configure.GUIAudioProcess(out var soundEmitter);

        process.ApplyProcess();
        yield return new WaitForSeconds(Configure.Duration);

        soundEmitter.AudioSourceProperty.IsPlaying.Should().BeTrue();
    }
}