using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PetBehaviourStateMachine : MonoBehaviour
{
    //keeps track of a PetBehaviourState Object
    protected PetBehaviourState State;

    public void SetState(PetBehaviourState state)
    {
        State = state;
        StartCoroutine(State.Start());
    }
}
