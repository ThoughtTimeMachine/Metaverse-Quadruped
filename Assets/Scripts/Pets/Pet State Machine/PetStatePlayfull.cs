using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStatePlayfull : PetBehaviorState
{
    //when this class is set as the current state the PlayState enum is set here by calling a struct or clas that maintains the float values for its current mood
    //or toys overide the state to play with the user
    //or if in this playfull state, then if the user has a ball it sends an event to the pet and checks if its in a playful state, if so then if the distance is within range of the anim then it will focus on the ball
    protected enum PlayState { toySolo, toySocial, playCatch, playSocial }
    protected PlayState playState;

    public PetStatePlayfull(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {

    }

    public override void Start()
    {
        //you can set the state of the petBehaviourSystem to another state here

    }
    //public override IEnumerator BlendAnimationState()
    //{
    //    float timeElapsed = 0;
    //    while

    //    switch (speedAnim)
    //        {
    //            case 0:
    //                animator.SetFloat("speedState",)
    //                break;
    //            case 1
    //        }
    //}
    //IEnumerator LerpSpeed(float endValue, float duration)
    //{
    //    float time = 0;
    //    float startValue = valueToChange;
    //    while (time < duration)
    //    {
    //        valueToChange = Mathf.Lerp(startValue, endValue, time / duration);
    //        time += Time.deltaTime;
    //        yield return null;
    //    }
    //    valueToChange = endValue;
    //}
}
