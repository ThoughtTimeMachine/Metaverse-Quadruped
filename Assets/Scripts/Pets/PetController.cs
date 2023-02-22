using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem.iOS;

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

    public bool isOKToFollowObject;
    [SerializeField] private float _interpoolationMultiplier = 0.75f;

    private float _petsCurrentSpeed;
    private float _movementWeight = .5f;

    private Vector3 lastPosition;

    private float previousRotation;
    private float rotationSpeed;

    AnimatorHelperFunctions animatorHelper;
    [SerializeField] string[] _aniamtorParameters;
    [SerializeField] private int _TurnBodyAnimParameterIndex = 0;
    float currentValueTurnBodyParameter;
    float StartValue;

    public float lerpSpeed;

    //PickUpSequence Constraints
    public MultiAimConstraint multiAimConstraintNeck;
    private float _extendHeadForBall;
    public bool isOKToPickUpObject;
    [SerializeField] private float _pickupSequenceSpeed;

    //private MultiAimConstraintJob jobNeck;

    public Transform followObjectTest;

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
        //setting the characters feet and body rotation correctly to slopes and terrain
        _ikFootBehavior.RotateCharacterFeet();
        _ikFootBehavior.RotateCharactertBody();
        _ikFootBehavior.CharacterHeightAdjustment();

        //get the rotation speed of our quadrapeds turns so we can interpoolate the animations for turning
        //rotationSpeed = Mathf.Abs(_pet.transform.eulerAngles.y - previousRotation) / Time.deltaTime;
        //Mathf.Clamp(rotationSpeed, 0, 1);
        //print("rotationSpeed: " + rotationSpeed);


        //get the current speed of our pet     
        _petsCurrentSpeed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        lastPosition = transform.position;

        //Set the blend tree for turn rotations
        // YaxisAnimationInfluence("Turn Body");
        //previousRotation = _pet.transform.eulerAngles.y;

        //controlls the animation states for movement blend tree
        //VelocityInfluenceOnMovementBlendtree("Movement Weight");

        if (isOKToFollowObject)
        {
            //change navmesh agents destination to folow the object
            FollowObject(PetInteractablesManager.ActiveObjectOfInterest);
            PickupSequence(PetInteractablesManager.ActiveObjectOfInterest);
        }
        //if (_pet.isStopped)
        //{
        //    print("Pet Is Stopped");
        //}
        //if the characters distance is less than specified ammount then begin ReadyForObjectMouthPickup
        //ADD IF STATEMENT CHECK IF OBJECT IS ALLREADY IN MOUTH AND FIGURE OUT WHEN TO REACTIVATE THAT BOOL OTHERWISE DISTANCE BASED METHOD WILL KEEP CALLING


        //float angle = transform.localEulerAngles.y;
        //angle = (angle > 180) ? angle - 360 : angle;
        //print("Less Than, angle: " + angle);
    }
    private void OnEnable()
    {
        PetBehaviorSystem.StateChange += StopRandomDestination;
    }
    private void OnDisable()
    {
        PetBehaviorSystem.StateChange -= StopRandomDestination;
    }
    private void FollowObject(Transform obj)
    {
        _pet.destination = obj.position;
        //float distance = Vector3.Distance(obj.position, transform.position);
        //if (distance > stopDistance)
        //{
        //    agent.destination = transform.position;
        //}
        //else
        //{
        //    agent.destination = player.position;
        //}
    }
    public void SetDestinationPosition(Transform destination)
    {
        //recalculates the navmesh pet/agent path to a updated destination position but does not move the agent
        SetDestinationPathCalculation(destination);

        //does not move pet/agent, just sets the _petDestination variable we need our pet/agent to go to, before we call _pet.destination
        _petDestination = destination.position;

    }
    public void SetDestinationPathCalculation(Transform destination)
    {
        _pet.SetDestination(destination.position);
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
        //sets _petDestination to a random position and moves the pet to a random destination
        _petDestination = RandomDestinationPosition();
        _pet.destination = _petDestination;
    }
    public void StopRandomDestination()
    {
        //if you whistle/ call the dogs name or other interuptable process
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
    private void PickupSequence(Transform target)
    {
        float dist = Vector3.Distance(target.position, transform.position);
        //print("Distance To Toy: "+ dist);

        //instead of coroutine, have it be distance based value changing as aproaching
        if (dist < 1.5f && isOKToPickUpObject)
        {
            StartCoroutine(PickupSequenceLerp());
        }
    }

    IEnumerator PickupSequenceLerp()
    {

        isOKToPickUpObject = false;
        float neckValue = 0f;
        float crouchValueStart = _animator.GetFloat("Movement Weight");
        float crouchValue = 0f;
        float time = 0f;
        while (time < 1)
        {
            neckValue = Mathf.Lerp(0, 1, time);
            crouchValue = Mathf.Lerp(crouchValueStart, -1, time);
            multiAimConstraintNeck.weight = neckValue;
            _animator.SetLayerWeight(1, neckValue);

            _animator.SetFloat("Movement Weight", crouchValue);
            time += Time.deltaTime * _pickupSequenceSpeed;
            yield return null;
        }
        time = 0f;
        while (time < 1)
        {
            neckValue = Mathf.Lerp(1, 0, time);
            crouchValue = Mathf.Lerp(-1, 0, time);
            multiAimConstraintNeck.weight = neckValue;
            _animator.SetLayerWeight(1, neckValue);

            _animator.SetFloat("Movement Weight", crouchValue);
            time += Time.deltaTime * _pickupSequenceSpeed;
            yield return null;
        }
    }

    private void VelocityInfluenceOnMovementBlendtree(string animatorParameter)
    {


        if (_petsCurrentSpeed < 0.01f)//0.03 was a good number from the _petsCurrentSpeed values that resulted in movement printed.
        {
            //Lerp _movementWeight to 0 for the idle animation when not moving
            if (_animator.GetFloat(animatorParameter) > 0f)
            {
                _movementWeight = Mathf.Lerp(_animator.GetFloat(animatorParameter), 0, Time.deltaTime / _interpoolationMultiplier);
                _animator.SetFloat(animatorParameter, _movementWeight);
            }
        }
        //Lerp _movementWeight to 1 for the walking animation when moving

        if (_petsCurrentSpeed > 0.01f)
        {
            if (_animator.GetFloat(animatorParameter) < 1f)
            {
                _movementWeight = Mathf.Lerp(_animator.GetFloat(animatorParameter), 1, Time.deltaTime / _interpoolationMultiplier);
                _animator.SetFloat(animatorParameter, _movementWeight);
            }
        }
    }
    private void YaxisAnimationInfluence(string animatorParameter)
    {
        if (_petsCurrentSpeed < 1f && _petsCurrentSpeed > 0)
        {


            if (rotationSpeed > 3.0f)
            {

                print("Current Rotation: " + _pet.transform.eulerAngles.y + "previousRotation: " + previousRotation);
                if (_pet.transform.eulerAngles.y < previousRotation + .5f)
                {
                    StartValue = _animator.GetFloat("Turn Body");
                    currentValueTurnBodyParameter = Mathf.Lerp(StartValue, -1, rotationSpeed * lerpSpeed);
                    print("Turn Left");

                    _animator.SetFloat("Turn Body", currentValueTurnBodyParameter);
                }

                else if (_pet.transform.eulerAngles.y > previousRotation)
                {
                    StartValue = _animator.GetFloat("Turn Body");
                    currentValueTurnBodyParameter = Mathf.Lerp(StartValue, 1, rotationSpeed * lerpSpeed);
                    _animator.SetFloat("Turn Body", currentValueTurnBodyParameter);
                    print("Turn Right");
                }

                else if (_pet.transform.eulerAngles.y == previousRotation)
                {
                    StartValue = _animator.GetFloat("Turn Body");
                    currentValueTurnBodyParameter = Mathf.Lerp(StartValue, 0, rotationSpeed * lerpSpeed);
                    _animator.SetFloat("Turn Body", currentValueTurnBodyParameter);
                }
            }

            else if (rotationSpeed < 3.0f)
            {
                StartValue = _animator.GetFloat("Turn Body");
                currentValueTurnBodyParameter = Mathf.Lerp(StartValue, 0, rotationSpeed * lerpSpeed);
                _animator.SetFloat("Turn Body", currentValueTurnBodyParameter);
            }
        }
    }

    //    private void MovementAlternativeTest()//run in update loop
    //{
    //    Vector3 direction = transform.di
    //     float distance = Vector3.Distance(destination, transform.position);

    //    if (distance > 0.1)
    //    {

    //        LookToward(destination, distance);
    //        float distanceBAsedSpeedModifier = GetSpeedModifier(distance);

    //        Vector3 movement = transform.forward * Time.deltaTime * distanceBAsedSpeedModifier;

    //        _pet.Move(movement);
    //    }
    //}

}
