using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStateGoToPlayer : PetBehaviorState
{
    public PetStateGoToPlayer(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {

    }
    void Start()
    {
        _petBehaviorSystem._petController.StartRandomDestinations();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
