using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This PetBehaviourSystem is part of a state machine that needs the PetCareBars class on the pet to determine state and functions in state classes
[RequireComponent(typeof(PetCareBars))]
public class PetBehaviourSystem : PetBehaviourStateMachine
{
    //this class sets the PetBehaviourState and PetBehaviourState calls a method with logic whenever it is set in this class
   
    private void Start()
    {
        //we are going to choose to start the beginning state to An Idle state for the pet
        SetState(new PetStateIdle(this));
    }
    private void Update()
    {
        foreach (var objectLocation in PetInteractablesManager.Interactables)
        { 
            //if 
        }
    }
}
