using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStateThirsty : PetBehaviorState
{
    public PetStateThirsty(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {
    }

    public override void Start()
    {
        _petBehaviorSystem._petController.SetDestinationPosition(_petBehaviorSystem._petInteractionManager.Waterdish);
        Debug.Log("Entered Thirsty State");
    }

    public void OnTriggerEnter(Collider objectCollidedWith)
    {
        if (objectCollidedWith.tag == "WaterBowl")
        {
            if (_petBehaviorSystem._currentState == PetBehaviorSystem.CurrentState.thirst)
            {
                Debug.Log("Collided With Water Bowl");
                //Petcontroller.ChangeAnimationState()
            }
        }
    }

}
