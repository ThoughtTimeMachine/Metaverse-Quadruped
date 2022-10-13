using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationUtilityClass : MonoBehaviour
{
    public static bool AnimatorIsPlaying(Animator animator)
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
