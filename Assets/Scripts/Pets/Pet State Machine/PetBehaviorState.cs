using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public virtual void Updatelogic()
    {

    }
}
