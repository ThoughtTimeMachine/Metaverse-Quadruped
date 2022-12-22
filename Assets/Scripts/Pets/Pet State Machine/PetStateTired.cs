using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStateTired : PetBehaviorState
{
    private const string _sleep = "Sleep";//include lay down
    public PetStateTired(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {
    }

    public override void Start()
    {
        //you can set the state of the petBehaviourSystem to another state here
        _petBehaviorSystem._petController.ChangeAnimationState(_sleep,0);
        Debug.Log("Entered Tired State");
    }


}
