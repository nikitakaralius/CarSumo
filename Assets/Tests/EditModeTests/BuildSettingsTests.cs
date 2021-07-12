using FluentAssertions;
using NUnit.Framework;
using UnityEngine.SceneManagement;

public class BuildSettingsTests
{
    [Test]
    public void SceneCountShouldBeOne()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        sceneCount.Should().Be(1);
    }
}