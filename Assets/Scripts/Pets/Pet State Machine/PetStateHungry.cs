using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStateHungry : PetBehaviourState
{
    public PetStateHungry(PetBehaviourSystem petBehaviourSystem) : base(petBehaviourSystem)
    {
    }

    public override IEnumerator Start()
    {
        //you can set the state of the petBehaviourSystem to another state here
        yield return _wait;

    }
}
