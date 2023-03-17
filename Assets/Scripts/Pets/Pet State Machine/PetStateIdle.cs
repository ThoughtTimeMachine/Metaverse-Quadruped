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
        //start idle and casually walk around the environment checking things out untill PetCarBars deplete or increase changing the state
    }

    private void RandomIdleDestination()
    {
        Debug.Log("Entered Idle State");
        _petBehaviorSystem._petController.StartRandomDestinations();        
    }

    public void Updatelogic()
    {

    }
}
