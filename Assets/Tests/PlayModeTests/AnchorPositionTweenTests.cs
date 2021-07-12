using System.Collections;
using CarSumo.GUI;
using UnityEngine;
using UnityEngine.TestTools;

public class AnchorPositionTweenTests
{
    [UnityTest]
    public IEnumerator WhenApplyingProcess_AndWaitUntilItsDone_AnchoredPositionShouldBeApproximatelyToRangeMax()
    {
        IGUIProcess process = Configure.AnchorPositionTween(out var tweenData, out var rectTransform);

        process.Apply();

        yield return new WaitForSeconds(tweenData.Duration);

        rectTransform.anchoredPosition.ShouldBeApproximately(tweenData.Range.Max);
    }

    [UnityTest]
    public IEnumerator WhenApplyingProcessTwice_AndWaitUntilBothDone_ThenAnchoredPositionShouldBeApproximatelyToRangeMin()
    {
        IGUIProcess process = Configure.AnchorPositionTween(out var tweenData, out var rectTransform);

        process.Apply();
        yield return new WaitForSeconds(tweenData.Duration);
        process.Apply();                             
        yield return new WaitForSeconds(tweenData.Duration);

        rectTransform.anchoredPosition.ShouldBeApproximately(tweenData.Range.Min);
    }
}