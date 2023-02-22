using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHelperFunctions : MonoBehaviour
{
    Animator _animator;
    // private float _lerpSpeed = 0.1f;
    private string[] _parameterNames;
    private WaitForEndOfFrame wait = new WaitForEndOfFrame();
    public AnimatorHelperFunctions(Animator anim, string[] parameterNames)
    {
        _parameterNames = parameterNames;
        _animator = anim;
        print(parameterNames[0]);
    }
    public void LerpAnimatorParameter(int animatorParamterIndex, float targetValue, float lerpSpeed)
    {
        StartCoroutine(LerpAnimatorToValue(animatorParamterIndex, targetValue, lerpSpeed));
    }

    IEnumerator LerpAnimatorToValue(int animatorParamterIndex, float targetValue, float lerpSpeed)
    {
        //lerps the artget animator Parameter to an end value while a condition is true
        float startValue = _animator.GetFloat("Turn Body");
        float currentValue = _animator.GetFloat("Turn Body");

        while (currentValue != targetValue)
        {
            //transform.position = Vector3.Lerp(Vector3.zero, _targetPosition, time);
            currentValue = Mathf.Lerp(startValue, targetValue, lerpSpeed);

            _animator.SetFloat("Turn Body", currentValue);
            yield return wait;
        }
    }
}
