using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStateIdle : PetBehaviourState
{

    public PetStateIdle(PetBehaviourSystem petBehaviourSystem) : base(petBehaviourSystem)
    {
    }

    public override IEnumerator Start()
    {
        //you can set the state of the petBehaviourSystem to another state here
        //start idleand casually walk around the environment checking things out untill PetCarBars deplete or increase changing the state
        yield return _wait;

    }
   
}
