using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStateCurious : PetBehaviorState
{
    public PetStateCurious(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {
    }
    public override void Start()
    {
        //find your owner
        //get into a playfull state animation
        //listen to the words no from wit.ai
        //atempt once more trying to play
        //start playing with a toy on your own
        Debug.Log("Entered Curious State");
    }
    //public void override IEnumerator Start()
    //{
    //    //you can set the state of the petBehaviourSystem to another state here
    //    yield return _wait;

    //}

}
