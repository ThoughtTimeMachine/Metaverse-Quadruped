using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PetBehaviorStateMachine : MonoBehaviour
{
    //keeps track of a PetBehaviourState Object
    protected PetBehaviorState State;
    public static Action changedState;

    public void SetState(PetBehaviorState state)
    {
        print("Changing State");
        changedState?.Invoke();
        State = state;
        State.Start(); //calls start like petStateIdle.Start() since there is no monobehaviour, Starts not automatically called on instantiation of this class       
    }
    private void Update()
    {
        if (State != null)
        {
            
            State.Updatelogic();
        }
    }
}

