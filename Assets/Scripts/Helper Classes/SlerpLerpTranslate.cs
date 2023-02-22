using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SlerpLerpTranslate : MonoBehaviour
{
   
    //public Transform _targetPosition;
    //public Transform _targetRotation;
    [Range(0.1f, 2.0f)] private float _lerpSpeed = 2.0f;
    //private float _currentPostion;
    //private float _targetCurvePosition;
    //private AnimationCurve _curve;
    //public Transform testTarget;

    public void RotateToPosition(Transform target)
    {
        StartCoroutine(RotationSlerp(target));
        StartCoroutine(PositionLerp(target));
    }
    IEnumerator RotationSlerp(Transform targetRotation)
    {
        Quaternion fromRotation = transform.rotation;
        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(fromRotation, Quaternion.Euler(targetRotation.rotation.eulerAngles), time);
            time += Time.deltaTime * _lerpSpeed;
            yield return null;
        }
    }
    IEnumerator PositionLerp(Transform targetPosition)
    {
     
        Vector3 startingPosition = transform.position;
        float time = 0;
        while (time < 1)
        {
            //transform.position = Vector3.Lerp(Vector3.zero, _targetPosition, time);
            transform.position = Vector3.Lerp(startingPosition, targetPosition.position, time);
            time += Time.deltaTime * _lerpSpeed;
            yield return null;
        }
    }


    //IEnumerator LerpAnimatorToValue(float startingValue, float targetValue,)
    //{
    //    float startValue = startingValue;
    //    float time = 0;
    //    while (time < 1)
    //    {
    //        //transform.position = Vector3.Lerp(Vector3.zero, _targetPosition, time);
    //        transform.position = Mathf.Lerp(startValue, targetValue, time);
    //        time += Time.deltaTime * _lerpSpeed;
    //        yield return null;
    //    }
    //}
    //IEnumerator RotationPositionSlerpLerp()
    //{
    //    Vector3 startingPosition = transform.position;
    //    Vector3 fromRotation = transform.rotation.eulerAngles;

    //    _currentPostion = Mathf.MoveTowards(_currentPostion,_targetCurvePosition,_lerpSpeed*Time.deltaTime);

    //    //transform.position = Vector3.Lerp(Vector3.zero, _targetPosition, _curve.Evaluate(_currentPostion));
    //    //transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.zero), Quaternion.Euler(_targetRotation), _curve.Evaluate(_currentPostion));
    //    transform.position = Vector3.Lerp(startingPosition, _targetPosition.position, _curve.Evaluate(_currentPostion));     
    //    transform.rotation = Quaternion.Lerp(Quaternion.Euler(fromRotation),Quaternion.Euler(_targetRotation.rotation.eulerAngles),_curve.Evaluate(_currentPostion));

    //    yield return null;
    //}
}
