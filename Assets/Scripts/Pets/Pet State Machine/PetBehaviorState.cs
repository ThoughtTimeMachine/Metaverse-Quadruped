using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PetBehaviorState : MonoBehaviour //every state has access to PetBehaviorSystem through this inheritance
{
    protected PetBehaviorSystem PetBehaviorSystem;

    public PetBehaviorState(PetBehaviorSystem petBehaviourSystem)
    {
        PetBehaviorSystem = petBehaviourSystem;
    }

    public virtual void Start()
    {

    }

 

}
