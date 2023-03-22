using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PetBehaviorSystem;

public class PetStateThirsty : PetBehaviorState
{
    public PetStateThirsty(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {
    }

    public override void Start()
    {
        //call a method to drop anything thats in the pets mouth

        _petBehaviorSystem._petController.SetDestination(_petBehaviorSystem._petInteractionManager.Waterdish);
        Debug.Log("Entered Thirsty State");
    }

    public void OnTriggerEnter(Collider objectCollidedWith)
    {
        if (objectCollidedWith.tag == "WaterBowl")
        {
            //if ()
            //{
            //    Debug.Log("Collided With Water Bowl");
            //    //Petcontroller.ChangeAnimationState()
            //}
        }
    }

    public override void Updatelogic()
    {

        if (_petBehaviorSystem._thirst < 1f)
        {
            _petBehaviorSystem.IncreaseStatusBarValue(StatusBar.thirsty);
        }
        else
        {
            Debug.Log("entering idle");
            _petBehaviorSystem.SetState(_petBehaviorSystem.PetBehaviorStates[(int)CurrentState.idle]);
            _petBehaviorSystem.IsInterruptibleState = true;
        }
    }
}
