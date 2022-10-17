using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetBehaviourSystem : PetBehaviourStateMachine
{
    //this class sets the PetBehaviourState and PetBehaviourState calls a method with logic whenever it is set in this class
    private void Start()
    {
        //we are going to choose to start the beginning state to An Idle state for the pet
        SetState(new PetStateIdle(this));
    } 
}
