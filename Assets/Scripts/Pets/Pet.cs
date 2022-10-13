using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    private int _age = 0;
    private float _speed = 1;
    [SerializeField] private PetPersonality _petPersonality;
    [SerializeField] private ParticleSystem _puke;
    [SerializeField] private ParticleSystem _pee;
    [SerializeField] private ParticleSystem _poop;
    [SerializeField] private GameObject _poopObject;
    [SerializeField] private GameObject _pukeObject; 
    void Update()
    {

    }

    protected void CurrentState()
    {

    }
    public void Eat(PetFood food)
    {
        //trigger animation state of random entry and sniff ect, then chewing animation then call destroy.
        ConsumeItem(food);
    }
    public void Sleep()
    {
        //trigger animation state for sleep
    }
    public void PlayWithToy(PetToy toy)
    {
        //trigger animation state of playing with a toy. Toys will need an enum that is selectable on the hierarchy to set if its chewable, throw aroundable, pushable ect
    }
    public void Catch(PetToy catchableToy)
    {
        //attempt to cathc the ball which is dependant on the speed of the pet and the velocity of the object
    }
    public void DoTrick(Tricks trick) //might be interface so alien pets can different tricks than regular pets
    {
        //Trigger animation state and Invoke a trick from the class
    }
    public void GoToBathroom()
    {
        //Trigger Particle System
    }
    public void Puke()
    {
        //Trigger Particle System
    }
    public void DestroyItem(GameObject itemToDestroy)
    {
       //destroy it if its not a consumable
    }
    public void ConsumeItem(PetFood food)
    {
        //Send object back to Object pool or destroy it if its not a consumable
    }
}
