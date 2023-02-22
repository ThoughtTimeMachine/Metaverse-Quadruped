using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PetBehaviorState 
{
    protected PetBehaviorSystem _petBehaviorSystem;

    public PetBehaviorState(PetBehaviorSystem petBehaviourSystem)
    {
        _petBehaviorSystem = petBehaviourSystem;
    }

    public virtual void Start()
    {

    }

}
