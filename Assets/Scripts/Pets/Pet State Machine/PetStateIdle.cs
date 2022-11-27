using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStateIdle : PetBehaviorState
{

    public PetStateIdle(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {
    }
    private void Update()
    {
        
    }
    public override void Start()
    {
        
        //you can set the state of the petBehaviourSystem to another state here
        //start idle and casually walk around the environment checking things out untill PetCarBars deplete or increase changing the state
       
    }

   //subscribe or get call from the petBehviorSystem delegate call on a state change in that class. We either disable our state or enable it.
}
