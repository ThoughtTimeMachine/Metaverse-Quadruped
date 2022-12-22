using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PetStateIdle : PetBehaviorState
{
  
    public PetStateIdle(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {
    }

    public override void Start()
    {      
        RandomIdleDestination();// Invoke reapeating with ability to stop. What determins the length of time when reaching the random destination and actions performed like sniff?
        //you can set the state of the petBehaviourSystem to another state here
        //start idle and casually walk around the environment checking things out untill PetCarBars deplete or increase changing the state
    }

    private void RandomIdleDestination()
    {
        _petBehaviorSystem._petController.StartRandomDestinations();
 
        Debug.Log("Entered Idle State");
    }
  
}
