using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PetBehaviorState : MonoBehaviour
{
    protected PetBehaviorSystem PetBehaviorSystem;

    public PetBehaviorState(PetBehaviorSystem petBehaviourSystem)
    {
        PetBehaviorSystem = petBehaviourSystem;
    }

    public virtual void Start()
    {

    }

    protected void LookForObject()//interface parameter that can supply position of object or palce to be at
    {
        //Implemented in the current state
    }
    protected void GoToDestination()//interface parameter that can supply position of object or palce to be at
    {
        
    }

}
