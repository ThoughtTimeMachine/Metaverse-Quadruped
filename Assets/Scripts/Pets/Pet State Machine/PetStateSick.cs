using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStateSick : PetBehaviorState
{
    //this class might be a sub state?
    public PetStateSick(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {
    }

    public override void Start()
    {
        //begin throwing up contents of food and trigger the vfx particle system
        Debug.Log("Entered Sick State");
    }
}

