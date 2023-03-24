using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStateFollowObjectOfInterest : PetBehaviorState
{
    public PetStateFollowObjectOfInterest(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {
    }

    public override void Start()
    {
        Debug.Log("Entered Sick State");
        PetController.isOKToFollowObject = true;
    }

    // Update is called once per frame
    public override void Updatelogic()
    {
        _petBehaviorSystem._petController.FollowObject(PetInteractablesManager.ActiveObjectOfInterest);
        
    }
}
