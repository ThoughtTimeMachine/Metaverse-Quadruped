using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//This class sets the currentState and PetBehaviourState derrived classes calls a method with logic whenever it is set in this class
public class PetBehaviorSystem : PetBehaviorStateMachine, IDataPersistence
{
    [Header("Status Bar Depletion Time")]
    public float LengthOfTime = 172800; //seconds in 48 hours
    //time fields for loading 
    private float _lastSavedTimed;

    //Pet Status Bars influence the CurrentState and SpeedAnimaton Enums
    //[Header("StatusBars")]
    private List<float> PetStatusBarsValue = new List<float>();
    private List<float> PetStatusBarsValueMultiplier = new List<float>();
    [SerializeField]
    private Image[] PetStatusBarsFill = new Image[7];

    private float _happiness = 1;
    private float _hunger = 1;
    private float _thirst = 1;
    private float _boredom = 1;
    private float _bathroom = 1;
    private float _energy = 1;
    private float _cleanliness = 1;

    private byte _happinessMultiplier = 1;
    private byte _hungerMultiplier = 1;
    private byte _thirstMultiplier = 1;
    private byte _boredomMultiplier = 1;
    private byte _bathroomMultiplier = 1;
    private byte _energyMultiplier = 1;
    private byte _cleanlinessMultiplier = 1;

    public enum StatusBars { happiness, hunger, thirsty, boredom, bathroom, energy, cleanliness }
    public StatusBars _statusBars;

    public enum CurrentState { idle, curious, tired, sleep, hungry, thirst, sick, playfull } // Binary tree to help determine state?
    public CurrentState _currentState { get; private set; }
    private bool IsInterruptibleState = true;
    private WaitForSeconds InterruptibleStateWait = new WaitForSeconds(2f);
    public enum SpeedAnimation { idle, walk, run, sprint }
    public SpeedAnimation speedAnim;

    //Maybe the core personality trait is influenced by how ofter your pet enters/ and or is in a CurrentState? Turn into Interfaces?
    public enum CorePersonalityTrait { Happy, Disobedient, Aggressive, Scared, Protective, HighAlert, Lazy, Bored }
    private CorePersonalityTrait _corePersonality;
    public bool IsSearchingForInteractable { get; private set; }


    //public CurrentState PetsCurrentState
    //{
    //    get { return _currentState; }
    //   set { _currentState = value; }
    //}
    private void Awake()
    {
        //build the dictionary of status bars and rate depletion values to itterate over in the update loop. We will set the petStatusBars fill from this dicitonarys values int he Tuple
        BuildStatusBarDictionary();
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
        DecreaseStatusBars();//possible use coroutine instead if its more performant.

        if (IsInterruptibleState) 
        { 
            DeterminState();  
        }
       
    }
    private void BuildStatusBarDictionary()
    {
        PetStatusBarsValue.Add(_happiness);
        PetStatusBarsValue.Add(_hunger);
        PetStatusBarsValue.Add(_thirst);
        PetStatusBarsValue.Add(_boredom);
        PetStatusBarsValue.Add(_bathroom);
        PetStatusBarsValue.Add(_energy);
        PetStatusBarsValue.Add(_cleanliness);

        PetStatusBarsValueMultiplier.Add(_happinessMultiplier);
        PetStatusBarsValueMultiplier.Add(_hungerMultiplier);
        PetStatusBarsValueMultiplier.Add(_thirstMultiplier);
        PetStatusBarsValueMultiplier.Add(_boredomMultiplier);
        PetStatusBarsValueMultiplier.Add(_bathroomMultiplier);
        PetStatusBarsValueMultiplier.Add(_energyMultiplier);
        PetStatusBarsValueMultiplier.Add(_cleanlinessMultiplier);
    }

    private void DecreaseStatusBars()
    {
        //loop the petStatusBarValue dictionary and access each PetStatusBar inside the tuple and decrease its fill by Time.deltaTime / 172800 (seconds in 48 hours) * depletionRate the Tuples Item2 which is the RateOverTime
        for (int i = 0; i <= PetStatusBarsValue.Count - 1; i++)
        {
            if (PetStatusBarsValue[i] > 0.01f)
            {
                PetStatusBarsValue[i] -= Time.deltaTime / LengthOfTime * PetStatusBarsValueMultiplier[i];
                PetStatusBarsFill[i].fillAmount = PetStatusBarsValue[i];
            }
        }
    }
    //SetStatusBarValue can be called to increase a status bars value. Examples woul be treats, toys, bowl of food, water, sleep ect.
    protected void SetStatusBarValue(float value, StatusBars statusBar)
    {
        switch (statusBar)
        {
            case StatusBars.happiness:
                if (_happiness < 1f)
                {
                    float statusBarCurrentValue = _happiness;
                    while (_happiness < _happiness + statusBarCurrentValue)
                    {
                        _happiness += Time.deltaTime / 3f;
                        PetStatusBarsFill[(int)StatusBars.happiness].fillAmount = _happiness;
                    }
                }
                break;

            case StatusBars.hunger:
                if (_hunger < 1f)
                {
                    float statusBarCurrentValue = _hunger;
                    while (_hunger < _hunger + statusBarCurrentValue)
                    {
                        _hunger += Time.deltaTime / 3f;
                        PetStatusBarsFill[(int)StatusBars.hunger].fillAmount = _hunger;
                    }
                }
                break;

            case StatusBars.thirsty:
                if (_thirst < 1f)
                {
                    float statusBarCurrentValue = _thirst;
                    while (_thirst < _thirst + statusBarCurrentValue)
                    {
                        _thirst += Time.deltaTime / 3f;
                        PetStatusBarsFill[(int)StatusBars.thirsty].fillAmount = _thirst;
                    }
                }
                break;

            case StatusBars.boredom:
                if (_boredom < 1f)
                {
                    float statusBarCurrentValue = _thirst;
                    while (_boredom < _boredom + statusBarCurrentValue)
                    {
                        _boredom += Time.deltaTime / 3f;
                        PetStatusBarsFill[(int)StatusBars.boredom].fillAmount = _boredom;
                    }
                }
                break;

            case StatusBars.bathroom:
                if (_bathroom < 1f)
                {
                    float statusBarCurrentValue = _thirst;
                    while (_bathroom < _bathroom + statusBarCurrentValue)
                    {
                        _bathroom += Time.deltaTime / 3f;
                        PetStatusBarsFill[(int)StatusBars.bathroom].fillAmount = _bathroom;
                    }
                }
                break;

            case StatusBars.energy:
                if (_energy < 1f)
                {
                    float statusBarCurrentValue = _thirst;
                    while (_energy < _energy + statusBarCurrentValue)
                    {
                        _energy += Time.deltaTime / 3f;
                        PetStatusBarsFill[(int)StatusBars.energy].fillAmount = _energy;
                    }
                }
                break;

            case StatusBars.cleanliness:
                if (_cleanliness < 1f)
                {
                    float statusBarCurrentValue = _thirst;
                    while (_cleanliness < _cleanliness + statusBarCurrentValue)
                    {
                        _cleanliness += Time.deltaTime / 3f;
                        PetStatusBarsFill[(int)StatusBars.cleanliness].fillAmount = _cleanliness;
                    }
                }
                break;
        }
    }
    protected void SetStatusBarMultiplierValue(float value, StatusBars statusBar)
    {
        switch (statusBar)
        {
            case StatusBars.happiness:
                PetStatusBarsValueMultiplier[(int)StatusBars.happiness] = value;
                break;

            case StatusBars.hunger:
                PetStatusBarsValueMultiplier[(int)StatusBars.hunger] = value;
                break;

            case StatusBars.thirsty:
                PetStatusBarsValueMultiplier[(int)StatusBars.thirsty] = value;
                break;

            case StatusBars.boredom:
                PetStatusBarsValueMultiplier[(int)StatusBars.boredom] = value;
                break;

            case StatusBars.bathroom:
                PetStatusBarsValueMultiplier[(int)StatusBars.bathroom] = value;
                break;

            case StatusBars.energy:
                PetStatusBarsValueMultiplier[(int)StatusBars.energy] = value;
                break;

            case StatusBars.cleanliness:
                PetStatusBarsValueMultiplier[(int)StatusBars.cleanliness] = value;
                break;
        }
    }

    private void DeterminState()
    {
        // need IsInterruptibleState logic figured out. 
        if (_thirst < .25f) 
        { 
            SetState(new PetStateThirsty(this)); 
        }

        else if (_hunger < .25f) 
        { 
            SetState(new PetStateHungry(this)); 
        }

        if (_energy < .15f) 
        { 
            SetState(new PetStateTired(this));
            IsInterruptibleState = false;
            StartCoroutine(InteruptableStateActivation());
        }

        else if (_bathroom < .15f) 
        { 
            SetState(new PetStateBathroom(this)); 
        }

        //for (int i = 0; i <= PetStatusBarsValue.Count - 1; i++)
        //{
        //    DeterminStateAlternativeVersion(PetStatusBarsValue[i]);
        //}
    }
    private IEnumerator InteruptableStateActivation()
    {
        while(_energy < .25f)
        {
            yield return InterruptibleStateWait;
        }
        IsInterruptibleState = true;
    }
    //Relational Pattern implementation  of method DeterminState()
    //private PetBehaviorState DeterminStateAlternativeVersion(float statusBarValue) =>
    //statusBarValue switch
    //{
    //    (> .32f) and (< 212) => new PetStateTired(this),
    //    < 32 => new PetStateThirsty(this),
    //    > 212 => new PetStateHungry(this),
    //    .32   => new PetStateBathroom(this),
    //    212 => new PetStateBathroom(this),
    //    _ => new PetStateIdle(this),
    //};

    public void SaveData(GameData data)
    {
        data.Happiness = _happiness;
        data.Hunger = _hunger;
        data.Thirst = _thirst;
        data.Boredom = _boredom;
        data.Bathroom = _bathroom;
        data.Energy = _energy;
        data.Cleanliness = _cleanliness;
        data.LastSavedTime = _lastSavedTimed;
    }
    public void LoadData(ref GameData data)
    {
        _happiness = data.Happiness;
        _hunger = data.Hunger;
        _thirst = data.Thirst;
        _boredom = data.Boredom;
        _bathroom = data.Bathroom;
        _energy = data.Energy;
        _cleanliness = data.Cleanliness;
        _lastSavedTimed = data.LastSavedTime;

        //load fill rates to correct values and save and load PetStatusBarsValueMultiplier
    }
}

