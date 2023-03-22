using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStateLayDown : PetBehaviorState
{
    public PetStateLayDown(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {
    }
    public override void Start()
    {
        Debug.Log("Entered Lay Down State");
        //give paw cross fade animation, maybe constraint, orientation
        _petBehaviorSystem._petController.ChangeAnimationState("lay down", 0);
    }

    // Update is called once per frame

}

