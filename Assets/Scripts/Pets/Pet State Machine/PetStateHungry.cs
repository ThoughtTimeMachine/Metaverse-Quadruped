using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PetBehaviorSystem;

public class PetStateHungry : PetBehaviorState
{
    public PetStateHungry(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {

    }

    public override void Start()
    {
        //call a method to drop anything thats in the pets mouth

        _petBehaviorSystem._petController.SetDestination(_petBehaviorSystem._petInteractionManager.FoodDish);
        Debug.Log("Entered Hungry State");
    }

    public void OnTriggerEnter(Collider objectCollidedWith)
    {
        if (objectCollidedWith.tag == "FoodBowl")
        {
            
        }

        else if (objectCollidedWith.tag == "FoodTreat")
        {
            //Petcontroller.ChangeAnimationState()
        }
    }
    public override void Updatelogic()
    {
        if (_petBehaviorSystem._hunger < 1f)
        {
            _petBehaviorSystem.IncreaseStatusBarValue(StatusBar.hunger);
        }
        else
        {
            Debug.Log("entering idle");
            _petBehaviorSystem.SetState(_petBehaviorSystem.PetBehaviorStates[(int)CurrentState.idle]);
            _petBehaviorSystem.IsInterruptibleState = true;
        }
    }
}
