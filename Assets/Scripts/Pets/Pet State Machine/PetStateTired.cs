using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PetBehaviorSystem;

public class PetStateTired : PetBehaviorState
{
    private const string _sleep = "sleep"; //include lay down
    public PetStateTired(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {
    }

    public override void Start()
    {
        Debug.Log("Entered Tired State");
        //call a method to drop anything thats in the pets mouth
        //you can set the state of the petBehaviourSystem to another state here
        _petBehaviorSystem._petController.ChangeAnimationState(_sleep, 0);

    }
    // Update is called once per frame
    public void Updatelogic()
    {
        if (_petBehaviorSystem._energy < 1f)
        {
            _petBehaviorSystem.IncreaseStatusBarValue(StatusBar.energy);
        }    
        else
        {
            Debug.Log("entering idle");
            _petBehaviorSystem.SetState(_petBehaviorSystem.PetBehaviorStates[(int)CurrentState.idle]);
            _petBehaviorSystem.IsInterruptibleState = true;
        }
    }

}
