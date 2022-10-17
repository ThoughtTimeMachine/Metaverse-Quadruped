using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PetBehaviourState
{
    protected PetBehaviourSystem PetBehaviourSystem;

    protected WaitForSeconds _wait = new WaitForSeconds(1f);
    public PetBehaviourState(PetBehaviourSystem petBehaviourSystem)
    {
        PetBehaviourSystem = petBehaviourSystem;
    }

    public virtual IEnumerator Start()
    {
        yield break;
    }

    public virtual IEnumerator Hungry()
    {
        yield break;
    }
    public virtual IEnumerator Thirsty()
    {
        yield break;
    }
    public virtual IEnumerator Playfull()
    {
        yield break;
    }
    public virtual IEnumerator Tired()
    {
        yield break;
    }
    public virtual IEnumerator Curious()
    {
        yield break;
    }
    public virtual IEnumerator Sick()
    {
        yield break;
    }
    public virtual IEnumerator Social()
    {
        //With other pets and or people
        yield break;
    }
    public virtual IEnumerator Bored()
    {
        yield break;
    }
}
