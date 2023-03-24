using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStateFollowAndPickUpObject : PetBehaviorState
{
    public PetStateFollowAndPickUpObject(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {
    }
    public override void Start()
    {
        //Set our Navmesh agents destination to the object and when within range to pick it up, get the object
        Debug.Log("Entered Follow And Pick Up Object State");
        _petBehaviorSystem._petController.ChangeAnimationState("Movement", 0);
        PetController.isOKToFollowObject = true;
        _petBehaviorSystem._petController.isOKToPickUpObject = true;
        Debug.Log("PetController.isOKToFollowObject = "+ PetController.isOKToFollowObject);
    }
    public override void Updatelogic()
    {
        Debug.Log("Pet Should be Following Object");
        //follows the ActiveObjectOfInterest and stops the Pet if they are within the specified range
        _petBehaviorSystem._petController.FollowObject(PetInteractablesManager.ActiveObjectOfInterest);

        //if pet is within pickup distance, it picks up the object in its mouth
        _petBehaviorSystem._petController.PickupSequence(PetInteractablesManager.ActiveObjectOfInterest);
    }
}


