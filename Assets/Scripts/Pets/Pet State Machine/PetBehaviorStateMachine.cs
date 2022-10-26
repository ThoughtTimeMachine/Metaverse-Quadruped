using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PetBehaviorStateMachine : MonoBehaviour
{
    //keeps track of a PetBehaviourState Object
    protected PetBehaviorState State;

    public void SetState(PetBehaviorState state)
    {
        State = state;
        State.Start();
    }
}
