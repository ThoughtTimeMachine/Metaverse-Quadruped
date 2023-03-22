using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PetBehaviorSystem;

public class PetStateBathroom : PetBehaviorState
{
    public PetStateBathroom(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {

    }

    public override void Start()
    {
        //call a method to drop anything thats in the pets mouth

        //untill trained just go on floor
        //depending on traning level, check if outdoor spot we can walk and wait at
        //whine
        //expect treat before maxing bathroom training;
        Debug.Log("Entered Bathroom State");
        //some animation for going pee or poop goes here, or VFX symbolising going to bathroom?
    }
    public override void Updatelogic()
    {

        if (_petBehaviorSystem._bathroom < 1f)
        {
            _petBehaviorSystem.IncreaseStatusBarValue(StatusBar.bathroom);
        }
        else
        {
            Debug.Log("entering idle");
            _petBehaviorSystem.SetState(_petBehaviorSystem.PetBehaviorStates[(int)CurrentState.idle]);
            _petBehaviorSystem.IsInterruptibleState = true;
        }
    }
}
