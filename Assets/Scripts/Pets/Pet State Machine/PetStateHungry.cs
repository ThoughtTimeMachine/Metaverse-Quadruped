using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStateHungry : PetBehaviorState
{
    public PetStateHungry(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {

    }

    public override void Start()
    {
        _petBehaviorSystem._petController.SetDestinationPosition(_petBehaviorSystem._petInteractionManager.FoodDish);
        Debug.Log("Entered Hungry State");
    }

    public void OnTriggerEnter(Collider objectCollidedWith)
    {
        if (objectCollidedWith.tag == "FoodBowl")
        {
            if (_petBehaviorSystem._currentState == PetBehaviorSystem.CurrentState.hungry)
            {
                //Petcontroller.ChangeAnimationState()
            }
        }

        else if (objectCollidedWith.tag == "FoodTreat")
        {
            //Petcontroller.ChangeAnimationState()
        }
    }
}
