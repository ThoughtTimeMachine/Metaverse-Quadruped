using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStatePlayfull : PetBehaviourState
{
    public PetStatePlayfull(PetBehaviourSystem petBehaviourSystem) : base(petBehaviourSystem)
    {
    }

    public override IEnumerator Start()
    {
        //you can set the state of the petBehaviourSystem to another state here
        yield return _wait;

    }
}
