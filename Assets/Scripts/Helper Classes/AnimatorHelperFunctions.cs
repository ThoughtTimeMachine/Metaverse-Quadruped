using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHelperFunctions : MonoBehaviour
{
    private static WaitForEndOfFrame wait = new WaitForEndOfFrame();
    public static void LerpAnimatorParameter(string AnimParameterName, float targetValue, float lerpSpeed, Animator animator)
    {
        Coroutine coroutine = CoroutineHelper.Instance.StartCoroutine(LerpAnimatorToValue(AnimParameterName, targetValue, lerpSpeed,animator));
    }

    static IEnumerator LerpAnimatorToValue(string AnimParameterName, float targetValue, float lerpSpeed,Animator animator)
    {
        //lerps the artget animator Parameter to an end value while a condition is true
        float startValue = animator.GetFloat(AnimParameterName);
        float currentValue = 0f;

        while (currentValue != targetValue)
        {
            currentValue = Mathf.Lerp(startValue, targetValue, lerpSpeed);

            animator.SetFloat(AnimParameterName, currentValue);
            yield return wait;
        }
    }
}
