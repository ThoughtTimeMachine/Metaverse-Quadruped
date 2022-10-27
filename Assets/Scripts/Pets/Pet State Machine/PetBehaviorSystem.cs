using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class sets the currentState and PetBehaviourState derrived classes calls a method with logic whenever it is set in this class
public class PetBehaviorSystem : PetBehaviorStateMachine
{
    //Pet Status Bars influence the CurrentState and SpeedAnimaton Enums
    private float _happiness;
    private float _hungry;
    private float _thirsty;
    private float _bored; //the longer in boredom, happiness slowly goes down
    private float _bathroom;
    private float _energy;
    private float _cleaness;
    protected enum SpeedAnimation { idle, walk, run, sprint }
    protected SpeedAnimation speedAnim;
    public enum CurrentState { idle, tired, thirsty, sick, playfull, hungry, curious,sleep }
    private CurrentState _currentState;

    public enum CorePersonalityTrait { Happy, Disobedient, Aggressive, Scared, Protective, HighAlert, Lazy, Bored }
    private CorePersonalityTrait _corePersonality;
    public bool IsSearchingForInteractable {get; private set;}
    public CurrentState PetsCurrentState
    {
        get { return _currentState; }
        set { _currentState = value; }
    }
    private void OnEnable()
    {

    }
    private void Start()
    {
        //we are going to choose to start the beginning state to An Idle state for the pet
        SetState(new PetStateIdle(this));
    }

    private void Update()
    {
        //if (PetsCurrentState != CurrentState.sleep)
        //{
        //}



    }

    //Is your pet happy, bored. hungry ect
    private void Happiness(float value)
    { }
    private void Hungry()
    { }
    private void Thirsty()
    { }
    private void Bored()
    { }
    private void Bathroom()
    { }
    private void Tired()
    { }
    private void Cleaness()
    { }
}
