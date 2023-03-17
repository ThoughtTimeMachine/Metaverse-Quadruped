using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStateSit : PetBehaviorState
{
    public PetStateSit(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {
    }
    void Start()
    {
        //enter sit antimation crossfade from controller or whatever
        _petBehaviorSystem._petController.ChangeAnimationState("sit", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
