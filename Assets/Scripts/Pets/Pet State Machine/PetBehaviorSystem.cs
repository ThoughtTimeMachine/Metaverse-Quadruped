using Meta.Conduit;
using Meta.WitAi;
using Meta.WitAi.Json;
using Newtonsoft.Json.Linq;
using OVRSimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

//This class sets the currentState and PetBehaviourState derrived classes calls a method with logic whenever it is set in this class
public class PetBehaviorSystem : PetBehaviorStateMachine, IDataPersistence
{
    //time fields for loading 
    [Header("Status Bar Depletion Time")]
    public float LengthOfTime = 172800; //seconds in 48 hours
    private float _lastSavedTimed;

    //Pet Status bars influence the CurrentState and SpeedAnimaton Enums
    private List<float> PetStatusBarsValue = new List<float>();
    private List<float> PetStatusBarsValueMultiplier = new List<float>();
    private Dictionary<int,PetBehaviorState> _petBehaviorStates = new Dictionary<int,PetBehaviorState>();
    public IReadOnlyDictionary<int, PetBehaviorState> PetBehaviorStates => _petBehaviorStates;
    [SerializeField]
    private Image[] PetStatusBarsFill = new Image[7];
    
    public float _happiness { get; private set; } = 1;
    public float _hunger { get; private set; } = 1;
    public float _thirst { get; private set; } = 1;
    public float _boredom { get; private set; } = 1;
    public float _bathroom { get; private set; } = 1;
    public float _energy { get; private set; } = 1;
    public float _cleanliness { get; private set; } = 1;

    public byte _happinessMultiplier { get; private set; } = 1;
    public byte _hungerMultiplier { get; private set; } = 1;
    public byte _thirstMultiplier { get; private set; } = 1;
    public byte _boredomMultiplier { get; private set; } = 1;
    public byte _bathroomMultiplier { get; private set; } = 1;
    public byte _energyMultiplier { get; private set; } = 1;
    public byte _cleanlinessMultiplier { get; private set; } = 1;

    public enum StatusBar { happiness, hunger, thirsty, boredom, bathroom, energy, cleanliness }
    public StatusBar _statusBar;

    public enum CurrentState { idle, tired, hungry, thirst, sick, playfull, bathroom, sit, paw,laydown, gotoplayer, followandpickupobject } // we can add more states to here
    public CurrentState _currentState { get; private set; }

    public bool IsInterruptibleState = true;
    private WaitForSeconds InterruptibleStateWait = new WaitForSeconds(2f);

    private int BathroomTrainedLevel;
    //Maybe the core personality trait is influenced by how often your pet enters/ and or is in a CurrentState? Turn into Interfaces?
    //public enum CorePersonalityTrait { Happy, Disobedient, Aggressive, Scared, Protective, HighAlert, Lazy, Bored }
    //private CorePersonalityTrait _corePersonality;
    public bool IsSearchingForInteractable { get; private set; }
    public PetController _petController { get; private set; }
    public PetInteractablesManager _petInteractionManager { get; private set; }

    //voice interaction stuff
    [SerializeField] public VoiceService Voice;

    //public static Action StateChange;

    public CurrentState PetsCurrentState
    {
        get { return _currentState; }
        set { _currentState = value; }
    }
    //IN THIS AREA OF CODE WE NEED TO CREATE A OBJECT LIKENESS SYSTEM FOR THE PET TO REMEMBER WHAT INTEREST IT AS AN OBJECT. THIS HELPS THE PET VISION CHANGE TO AN OBJECT OF INTEREST
    //
    //
    //
    //
    private void Awake()
    {
        _petController = gameObject.GetComponent<PetController>();
        _petInteractionManager = GameObject.FindObjectOfType<PetInteractablesManager>();

        //build the dictionary of status bars and rate depletion values to itterate over in the update loop. We will set the petStatusBars fill from this dicitonarys values int he Tuple
        InitializeBehaviorState();
    }
    private void OnDisable()
    {
        Voice.VoiceEvents.OnResponse.RemoveListener(VoiceInteraction);
    }
    private void Start()
    {
        if (!Voice) Voice = FindObjectOfType<VoiceService>();
        Voice.VoiceEvents.OnResponse.AddListener(VoiceInteraction);

        //set a starting state for the quadruped 
        _currentState = CurrentState.idle;
        SetState(_petBehaviorStates[(int)_currentState]);    
    }

    private void Update()
    {
        base.Update();
        //DecreaseStatusBarsUI();
        //updateStatusBarValuesFromList();

        if (IsInterruptibleState)
        {
            DeterminState();
        }
    }
    private void InitializeBehaviorState()
    {
        //initialize and cache our states and status bar variables into a dictionary
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

        _petBehaviorStates.Add((int)CurrentState.idle, new PetStateIdle(this));
        _petBehaviorStates.Add((int)CurrentState.tired, new PetStateTired(this));
        _petBehaviorStates.Add((int)CurrentState.hungry, new PetStateHungry(this));
        _petBehaviorStates.Add((int)CurrentState.thirst, new PetStateThirsty(this));
        _petBehaviorStates.Add((int)CurrentState.sick, new PetStateSick(this));
        _petBehaviorStates.Add((int)CurrentState.playfull, new PetStatePlayfull(this));
        _petBehaviorStates.Add((int)CurrentState.bathroom, new PetStateBathroom(this));
        _petBehaviorStates.Add((int)CurrentState.sit, new PetStateSit(this));
        _petBehaviorStates.Add((int)CurrentState.paw, new PetStatePaw(this));
        _petBehaviorStates.Add((int)CurrentState.laydown, new PetStateLayDown(this));
        _petBehaviorStates.Add((int)CurrentState.gotoplayer, new PetStateGoToPlayer(this));
        _petBehaviorStates.Add((int)CurrentState.followandpickupobject, new PetStateFollowAndPickUpObject(this));
    }

    public void BathroomTrainingLevelIncrease()
    {
        BathroomTrainedLevel++;
    }
    private void DecreaseStatusBarsUI()
    {
        //loop the petStatusBarValue dictionary and access each PetStatusBar inside the tuple and decrease its fill by Time.deltaTime / 172800 (seconds in 48 hours) * depletionRate the Tuples Item2 which is the RateOverTime
        //
        for (int i = 0; i <= PetStatusBarsValue.Count - 1; i++)
        {
            if (PetStatusBarsValue[i] > 0.01f)
            {
                PetStatusBarsValue[i] -= Time.deltaTime / LengthOfTime * PetStatusBarsValueMultiplier[i];
                PetStatusBarsFill[i].fillAmount = PetStatusBarsValue[i];
            }
        }
    }
    
    private void updateStatusBarValuesFromList()
    {
        for (int i = 0; i <= PetStatusBarsValue.Count - 1; i++)
        {

            _happiness = PetStatusBarsValue[i];
            _hunger = PetStatusBarsValue[i];
            _thirst = PetStatusBarsValue[i];
            _boredom = PetStatusBarsValue[i];
            _bathroom = PetStatusBarsValue[i];
            _energy = PetStatusBarsValue[i];
            _cleanliness = PetStatusBarsValue[i];
        }
    }
   
    public void IncreaseStatusBarValue( StatusBar statusBar)
    {
        //SetStatusBarValue can be called to increase a status bars value, just pass in the type of StatusBar you want to update.
        //Examples of interactions that increase status bar value could be: treats, toys, bowl of food, water, sleep ect.
        switch (statusBar)
        {
            case StatusBar.happiness:
                if (_happiness < 1f)
                {
                    float statusBarCurrentValue = _happiness;
                    while (_happiness < _happiness + statusBarCurrentValue)
                    {
                        _happiness += Time.deltaTime / 3f;
                        PetStatusBarsFill[(int)StatusBar.happiness].fillAmount = _happiness;
                    }
                }
                break;

            case StatusBar.hunger:
                if (_hunger < 1f)
                {
                    float statusBarCurrentValue = _hunger;
                    while (_hunger < _hunger + statusBarCurrentValue)
                    {
                        _hunger += Time.deltaTime / 3f;
                        PetStatusBarsFill[(int)StatusBar.hunger].fillAmount = _hunger;
                    }
                }
                break;

            case StatusBar.thirsty:
                if (_thirst < 1f)
                {
                    float statusBarCurrentValue = _thirst;
                    while (_thirst < _thirst + statusBarCurrentValue)
                    {
                        _thirst += Time.deltaTime / 3f;
                        PetStatusBarsFill[(int)StatusBar.thirsty].fillAmount = _thirst;
                    }
                }
                break;

            case StatusBar.boredom:
                if (_boredom < 1f)
                {
                    float statusBarCurrentValue = _boredom;
                    while (_boredom < _boredom + statusBarCurrentValue)
                    {
                        _boredom += Time.deltaTime / 3f;
                        PetStatusBarsFill[(int)StatusBar.boredom].fillAmount = _boredom;
                    }
                }
                break;

            case StatusBar.bathroom:
                if (_bathroom < 1f)
                {
                    float statusBarCurrentValue = _bathroom;
                    while (_bathroom < _bathroom + statusBarCurrentValue)
                    {
                        _bathroom += Time.deltaTime / 3f;
                        PetStatusBarsFill[(int)StatusBar.bathroom].fillAmount = _bathroom;
                    }
                }
                break;

            case StatusBar.energy:
                if (_energy < 1f)
                {
                    float statusBarCurrentValue = _energy;
                    while (_energy < _energy + statusBarCurrentValue)
                    {
                        _energy += Time.deltaTime / 3f;
                        PetStatusBarsFill[(int)StatusBar.energy].fillAmount = _energy;
                    }
                }
                break;

            case StatusBar.cleanliness:
                if (_cleanliness < 1f)
                {
                    float statusBarCurrentValue = _cleanliness;
                    while (_cleanliness < _cleanliness + statusBarCurrentValue)
                    {
                        _cleanliness += Time.deltaTime / 3f;
                        PetStatusBarsFill[(int)StatusBar.cleanliness].fillAmount = _cleanliness;
                    }
                }
                break;
        }
    }
    protected void SetStatusBarMultiplierValue(float value, StatusBar statusBar)
    {
        //use this to increase or decrese depletion of a Status Bar on the Quadruped
        switch (statusBar)
        {
            case StatusBar.happiness:
                PetStatusBarsValueMultiplier[(int)StatusBar.happiness] = value;
                break;

            case StatusBar.hunger:
                PetStatusBarsValueMultiplier[(int)StatusBar.hunger] = value;
                break;

            case StatusBar.thirsty:
                PetStatusBarsValueMultiplier[(int)StatusBar.thirsty] = value;
                break;

            case StatusBar.boredom:
                PetStatusBarsValueMultiplier[(int)StatusBar.boredom] = value;
                break;

            case StatusBar.bathroom:
                PetStatusBarsValueMultiplier[(int)StatusBar.bathroom] = value;
                break;

            case StatusBar.energy:
                PetStatusBarsValueMultiplier[(int)StatusBar.energy] = value;
                break;

            case StatusBar.cleanliness:
                PetStatusBarsValueMultiplier[(int)StatusBar.cleanliness] = value;
                break;
        }
    }
    private string FirstWitEntityName(WitResponseNode response)
    {
        //returns the First Entity Name from the WitResponseNode
        string jsonString = response.ToString();
        JObject responseObject = JObject.Parse(jsonString);
        JObject entitiesObject = (JObject)responseObject["entities"];

        JProperty firstEntityProperty = entitiesObject.Properties().First();
        JArray firstEntityArray = (JArray)firstEntityProperty.Value;
        JObject firstEntity = (JObject)firstEntityArray[0];
        string entityName = (string)firstEntity["name"];
        return entityName;
    }
    protected void VoiceInteraction(WitResponseNode response)
    {
        string name = FirstWitEntityName(response);
        //This subscribed event is ok if it changes the interaction a pet is doing, even if the DetermineState method is about to enter into a non interruptable state since the DetermineState runs in the Update() loop
        var entity = WitResultUtilities.GetFirstEntityValue(response, name+":"+name);

        print("FirstWitEntityName from response: " + FirstWitEntityName(response));
        //NEED TO EXTRACT THE VALUE OF WHATEVER THE ENTITY IS. POSSIBLY USE UNIVERSAL GET FIRST ENTITY VALUE IF ITS NOT A MULTI RESPONSE ISSUE/NEED
        //drop_object
        //go_to_player
        //pick_up
        //animation

        //turn the entire method below into a dictionary call that access the state with the entity value as the key
        if (IsInterruptibleState)
        {   
                print("entity: "+ entity);
                switch (entity)
                    {
                        case "sit":
                            print("Pet Should Sit Now");
                            _currentState = CurrentState.sit;
                            SetState(_petBehaviorStates[(int)_currentState]);
                        break;

                        case "paw":
                            print("Pet Should Give paw Now");
                            _currentState = CurrentState.paw;
                            SetState(_petBehaviorStates[(int)_currentState]);
                        break;

                        case "kevin":
                            print("Go To Player");
                            _currentState = CurrentState.gotoplayer;
                            SetState(_petBehaviorStates[(int)_currentState]);
                    break;

                        case "drop":
                            PetInteractablesManager.ActiveObjectOfInterest.GetComponent<ICarryDropable>().Drop();
                    break;
                            //could use a multi value event here for get the object, then decide what it is within the followandpickupobject state
                        case "get":
                            print("Pet Getting ball");
                            _currentState = CurrentState.followandpickupobject;
                            SetState(_petBehaviorStates[(int)_currentState]);
                    break;

                        case "lay down":
                            print("Pet Should lay Down");
                            _currentState = CurrentState.laydown;
                            SetState(_petBehaviorStates[(int)_currentState]);
                    break;
                    }     
        }
    }
    private void DeterminState()
    {
        //if we can interrupt the state then we need to check if our quadraped has a priority state they should be in based on their status bars depletion level
        if (IsInterruptibleState)
        {
            //we will go down a list of priority states and take care of them, with sleeping being the end state to enter if needed, after all other states are staisfied
            if (_thirst < .25f)
            {
                Debug.Log("_thirst = " + _thirst);
                SetToPriorityState(_bathroom, .25f, CurrentState.bathroom);
            }

            else if (_hunger < .25f)
            {
                Debug.Log("_hunger < .25f");
                SetToPriorityState(_bathroom, .25f, CurrentState.hungry);
            }


            else if (_bathroom < .15f)
            {
                Debug.Log("_bathroom < .15f");
                SetToPriorityState(_bathroom, 1f, CurrentState.bathroom);
            }

            else if (_energy < .15f)
            {
                Debug.Log("_energy < .15f");
                SetToPriorityState(_energy, .25f, CurrentState.tired);
            }
        }     
    }
    private void SetToPriorityState(float statValue,float time, CurrentState currentState)
    {
        //each priority state autoimatically enters back into the idle state after its reaches a maximum threshhold within that state class.
        IsInterruptibleState = false;
        _currentState = currentState;
       // StateChange.Invoke();
        SetState(_petBehaviorStates[(int)_currentState]);
       // StartCoroutine(EnterNonInteruptableState(statValue, 1f));
    }
    private IEnumerator EnterNonInteruptableState(float statusBar, float ammount)
    {
        while (statusBar < ammount)
        {
            yield return InterruptibleStateWait;
        }
        Debug.Log("InteruptableStateActivation: True");
        IsInterruptibleState = true;
    }

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
        //_happiness = data.Happiness;
        //_hunger = data.Hunger;
        //_thirst = data.Thirst;
        //_boredom = data.Boredom;
        //_bathroom = data.Bathroom;
        //_energy = data.Energy;
        //_cleanliness = data.Cleanliness;
        //_lastSavedTimed = data.LastSavedTime;

        //load fill rates to correct values and save and load PetStatusBarsValueMultiplier
    }
}

