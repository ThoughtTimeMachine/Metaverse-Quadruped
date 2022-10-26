using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStateCurious : PetBehaviorState
{
    public PetStateCurious(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {
    }

    //public void override IEnumerator Start()
    //{
    //    //you can set the state of the petBehaviourSystem to another state here
    //    yield return _wait;

    //}
}
