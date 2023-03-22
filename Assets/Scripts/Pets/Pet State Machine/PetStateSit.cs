using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStateSit : PetBehaviorState
{
    public PetStateSit(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {
    }
    public override void Start()
    {
        Debug.Log("Entered Sitting State");
        //enter sit antimation crossfade from controller or whatever
        _petBehaviorSystem._petController.ChangeAnimationState("sit", 0);
    }
    
}
