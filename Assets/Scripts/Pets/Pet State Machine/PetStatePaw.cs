using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStatePaw : PetBehaviorState
{
    public PetStatePaw(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {
    }
    public override void Start()
    {
        Debug.Log("Entered Paw State");
        //give paw cross fade animation, maybe constraint, orientation
        _petBehaviorSystem._petController.ChangeAnimationState("paw", 0);
    }

    // Update is called once per frame

}
