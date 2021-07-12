using System.Collections;
using CarSumo.GUI;
using FluentAssertions;
using UnityEngine;
using UnityEngine.TestTools;

public class VerticalLayoutSpacingTweenTests
{
    [UnityTest]
    public IEnumerator WhenApplyingProcess_AndWaitUntilItsDone_ThenGroupSpacingShouldBeAsTweenDataRangeMax()
    {
        IGUIProcess process = Configure.VerticalLayoutSpacingTween(out var group, out var tweenData);

        process.Apply();

        yield return new WaitForSeconds(tweenData.Duration);

        group.spacing.Should().Be(tweenData.Range.Max);
    }

    [UnityTest]
    public IEnumerator WhenApplyingProcessTwice_AndWaitUntilAllDone_ThenGroupSpacingShouldBeAsTweenDataRangeMin()
    {
        // Arrange
        IGUIProcess process = Configure.VerticalLayoutSpacingTween(out var group, out var tweenData);

        // Act.
        process.Apply();
        yield return new WaitForSeconds(tweenData.Duration);
        process.Apply();
        yield return new WaitForSeconds(tweenData.Duration);

        // Assert.
        group.spacing.Should().Be(tweenData.Range.Min);
    }
}