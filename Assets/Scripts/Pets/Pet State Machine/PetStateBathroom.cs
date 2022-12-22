using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStateBathroom : PetBehaviorState
{
    public PetStateBathroom(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {

    }

    public override void Start()
    {
        //untill trained just go on floor
        //depending on traning level, check if outdoor spot we can walk and wait at
        //whine
        //expect treat before maxing bathroom training;
        Debug.Log("Entered Bathroom State");
    }
}
