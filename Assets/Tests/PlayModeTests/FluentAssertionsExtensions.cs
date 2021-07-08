using FluentAssertions;
using UnityEngine;

public static class FluentAssertionsExtensions
{
    public static void ShouldBeApproximately(this Vector2 actual, Vector2 expected, float delta = Vector2.kEpsilon)
    {
        actual.x.Should().BeApproximately(expected.x, delta);
        actual.y.Should().BeApproximately(expected.y, delta);
    }
}