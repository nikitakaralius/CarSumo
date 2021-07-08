using System.Collections;
using CarSumo.GUI;
using FluentAssertions;
using UnityEngine;
using UnityEngine.TestTools;

public class ImageTransparencyTweenTests
{
    [UnityTest]
    public IEnumerator WhenApplyingProcess_AndImageTransparentOnStart_ThenAlphaShouldBe1()
    {
        var image = Create.Image();
        IGUIProcess process = Configure.ImageTransparencyTween(image, transparentOnStart: true);

        process.ApplyProcess();
        yield return new WaitForSeconds(Configure.Duration);

        image.color.a.Should().Be(1.0f);
    }

    [UnityTest]
    public IEnumerator WhenApplyingProcessTwice_AndImageTransparentOnStart_ThenAlphaShouldBe0()
    {
        var image = Create.Image();
        IGUIProcess process = Configure.ImageTransparencyTween(image, transparentOnStart: true);

        process.ApplyProcess();
        yield return new WaitForSeconds(Configure.Duration);
        process.ApplyProcess();                             
        yield return new WaitForSeconds(Configure.Duration);
        
        image.color.a.Should().Be(0.0f);

    }

    [UnityTest]
    public IEnumerator WhenApplyingProcess_AndImageNotTransparentOnStart_ThenAlphaShouldBe0()
    {
        var image = Create.Image();
        IGUIProcess process = Configure.ImageTransparencyTween(image, transparentOnStart: false);

        process.ApplyProcess();
        yield return new WaitForSeconds(Configure.Duration);

        image.color.a.Should().Be(0.0f);
    }

    [UnityTest]
    public IEnumerator WhenApplyingProcessTwice_AndNotImageTransparentOnStart_ThenAlphaShouldBe1()
    {
        var image = Create.Image();
        IGUIProcess process = Configure.ImageTransparencyTween(image, transparentOnStart: true);

        process.ApplyProcess();
        yield return new WaitForSeconds(Configure.Duration);
        process.ApplyProcess();                             
        yield return new WaitForSeconds(Configure.Duration);
        
        image.color.a.Should().Be(1.0f);

    }
}