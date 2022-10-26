using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStateSick : PetBehaviorState
{
    public PetStateSick(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {
    }

    public override void Start()
    {
        //you can set the state of the petBehaviourSystem to another state here

    }
}

