using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PetBehaviorState : MonoBehaviour
{
    protected PetBehaviorSystem PetBehaviorSystem;//where is this implemented?
    protected WaitForSeconds _wait = new WaitForSeconds(1f);

    public PetBehaviorState(PetBehaviorSystem petBehaviourSystem)
    {
        PetBehaviorSystem = petBehaviourSystem;
    }
   
    public virtual void Start()
    {
 
    }

    public virtual void Hungry()
    {

    }
    public virtual void Thirsty()
    {
 
    }
    public virtual void Playfull()
    {
      
    }
    public virtual void Tired()
    {
        
    }
    public virtual void Curious()
    {
    
    }
    public virtual void Sick()
    {
  
    }
    public virtual void Social()
    {
        //With other pets and or people
   
    }
    public virtual void Bored()
    {
    
    }

    public virtual void BlendAnimationState()
    {
   
    }
}
