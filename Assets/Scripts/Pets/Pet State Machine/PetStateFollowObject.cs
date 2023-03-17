using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStateFollowObject : PetBehaviorState
{
    private Transform tranform;
    public PetStateFollowObject(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {
    }

    public override void Start()
    {
        Debug.Log("Entered Sick State");
        PetController.isOKToFollowObject = true;
    }

    // Update is called once per frame
    public void Updatelogic()
    {
        _petBehaviorSystem._petController.FollowObject(PetInteractablesManager.ActiveObjectOfInterest);
        //_petBehaviorSystem._petController.PickupSequence(PetInteractablesManager.ActiveObjectOfInterest);
    }
}
