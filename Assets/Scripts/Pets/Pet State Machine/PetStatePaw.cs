using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStatePaw : PetBehaviorState
{
    public PetStatePaw(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {
    }
    void Start()
    {
        //give paw cross fade aniamtion, maybe constraint, orientation
        _petBehaviorSystem._petController.ChangeAnimationState("paw", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
