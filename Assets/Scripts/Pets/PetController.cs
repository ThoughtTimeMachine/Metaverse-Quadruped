using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(IKFootBehavior))]
[RequireComponent(typeof(Animator))]
public class PetController : MonoBehaviour
{
    private int _random = 0;
    private float _speed;
    [SerializeField] private NavMeshAgent _pet;
    public static Transform _petTransform;
    [SerializeField] private Transform _parentTransform;

    [SerializeField] private Animator _animator;
    [SerializeField] private string _animatorParameterForTurning = "Turn Body";
    public enum DestinationAnimation { idle, walk, run, sprint }
    private DestinationAnimation _destinationAnim;
    public enum TailAnimation { wag, wagFast, wagBroad }
    private TailAnimation _tailAnimation;

    public enum HeadAnimation { sniff, sniffGround, affection, disobediant, shame, PetHead }
    public HeadAnimation _headAnimation;

    private string CurrentAnimationState;
    public Vector3 _petDestination = new Vector3();
    private WaitForSeconds _waitTime = new WaitForSeconds(2f);
    private IKFootBehavior _ikFootBehavior;
    //subscripe StopRandomDestination to PetBehaviourSystem state changes with an observer pattern
    [SerializeField]
    private PetInteractablesManager _petInteractionManager;

    [SerializeField] private bool _updateRotation;
    private float previousYRotation = 0f;
    private void Awake()
    {
        _pet = gameObject.GetComponentInParent<NavMeshAgent>();
        _petTransform = gameObject.GetComponent<Transform>();
        _ikFootBehavior = gameObject.GetComponent<IKFootBehavior>();
        _parentTransform = GetComponentInParent<Transform>();
        _animator = gameObject.GetComponent<Animator>();

    }
    //might be able to use scriptable object instead
    private const string _walk = "walk", _walkLeft = "walk_left", _walkRight = "walk_right", _run = "run", _runLeft = "run_left", _runRight = "run_right", _sprint = "sprint", _sprintLeft = "sprint_left",
        _sprintRight = "sprint_right", _tailWag = "tail_wag", _tailWagBroad = "tail_wag_broad", _tailWagFast = "tail_wag_fast", _tailSleep = "tail_sleep", _tailScared = "tail_scared", _chew = "chew",
        _chewOnObject = "chew_on_object", _closeMouth = "close_mouth", _yawn = "yawn", _headSniff = "head_sniff", _headShame = "head_shame";
    private void Start()
    {
        _speed = (float)_destinationAnim;
        if (!_updateRotation)
        {
            _pet.updateRotation = false;
        }
    }
    private void Update()
    {
        _ikFootBehavior.RotateCharacterFeet();
        _ikFootBehavior.RotateCharactertBody();
        _ikFootBehavior.CharacterHeightAdjustment();

        //Set the blend tree for 
        YaxisAnimationInfluence("Turn Body");
    }
    private void OnEnable()
    {
        PetBehaviorSystem.StateChange += StopRandomDestination;
    }
    private void OnDisable()
    {
        PetBehaviorSystem.StateChange -= StopRandomDestination;
    }

    public void SetDestinationPosition(Transform destinations)
    {
        _petDestination = destinations.position;
        SetDestinationPathCalculation(destinations);
    }
    public void SetDestinationPathCalculation(Transform destinations)
    {
        _pet.SetDestination(destinations.position);
    }
    //IEnumerator GoToDestination()
    //{
    //    while (Mathf.Approximately(_pet.remainingDistance, 0f))
    //    {
    //        _pet.destination = _petDestination;
    //        yield return null;
    //    }
    //}

    public void StartRandomDestinations()
    {
        InvokeRepeating("RandomDestination", 0f, 15f);
    }
    public void RandomDestination()
    {
        _petDestination = RandomDestinationPosition();
        _pet.destination = _petDestination;
    }
    public void StopRandomDestination()//if you whistle/ call the dogs name or other interuptable process
    {
        CancelInvoke("RandomDestination");
    }
    private Vector3 RandomDestinationPosition()
    {
        if (_petInteractionManager.StaticObjectsOfCuriosity.Count != null)
        {
            //this is not random, needs to change
            _random = (_random + 1) % _petInteractionManager.StaticObjectsOfCuriosity.Count;
            _petDestination = _petInteractionManager.StaticObjectsOfCuriosity[_random].position;
            return _petDestination;
        }
        else return transform.position;
        Debug.Log("_petInteractionManager.StaticObjectsOfCuriosity.Count is null, retuning new vector3(0,0,0)");
    }

    public void ChangeAnimationState(string animation, int layer)
    {
        if (CurrentAnimationState != animation)
        {
            _animator.CrossFade(animation, .25f, layer);
        }
    }
    //no matter if the animation is a Idle,walk or run turn, this will adjust the same animator parameter float the animation uses in the blend tree.
    private void YaxisAnimationInfluence(string animatorParameter)
    {
        // Get the current y-axis rotation

        float currentYRotation = _parentTransform.eulerAngles.y;
        float currentParamterValue = _animator.GetFloat(animatorParameter);
        // Check if the y-axis rotation is increasing or decreasing
        if (currentYRotation > previousYRotation + 0.1f)
        {
            print("Turning Right, currentParamterValue: " + currentParamterValue);
            _animator.SetFloat(animatorParameter, currentParamterValue + 0.01f);
            if (currentParamterValue > 1f) { _animator.SetFloat(animatorParameter, 1f); }
        }
        else if (currentYRotation < previousYRotation - 0.1f)
        {
            print("Turning Left, currentParamterValue: " + currentParamterValue);
            _animator.SetFloat(animatorParameter, currentParamterValue - 0.01f);
            if (currentParamterValue < -1f) { _animator.SetFloat(animatorParameter, -1f); }          
        }
        else if (currentYRotation < -0.05f)
        {
            print("Not Turning At All, currentParamterValue: " + currentParamterValue);
            _animator.SetFloat(animatorParameter, currentParamterValue + 0.01f);
            if (currentParamterValue > 0f) { _animator.SetFloat(animatorParameter, 0f); }
        }
        else if (currentYRotation > 0.1f)
        {
            print("Not Turning At All, currentParamterValue: " + currentParamterValue);
            _animator.SetFloat(animatorParameter, currentParamterValue - 0.01f);
            if (currentParamterValue < 0f) { _animator.SetFloat(animatorParameter, 0f); }
        }
        // Update the previous y-axis rotation
        previousYRotation = currentYRotation;
    }

    private void MovementAlternativeTest()//run in update loop
    {
        // Vector3 direction = transform.di
        // float distance = Vector3.Distance(destination, transform.position);

        // if(distance >0.1)
        //{

        //LookToward(destination,distance);
        //float distanceBAsedSpeedModifier = GetSpeedModifier(distance);

        // Vector3 movement = transform.forward * Time.deltaTime * distanceBAsedSpeedModifier;

        //_pet.Move(movement);
        // }
    }

}
