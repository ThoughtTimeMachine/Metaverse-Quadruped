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
        //call a method to drop anything thats in the pets mouth

        //begin throwing up contents of food and trigger the vfx particle system
        Debug.Log("Entered Sick State");
    }
    public void Updatelogic()
    {

    }
}

