using Conduit.Generated;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;


[RequireComponent(typeof(IKFootBehavior))]
[RequireComponent(typeof(Animator))]
public class PetController : Singleton<PetController>
{
    
    private int _random = 0;
    private float _speed;
    [SerializeField] private NavMeshAgent _pet;
    public static Transform _petTransform;
    [SerializeField] private Transform _parentTransform;
    public Transform player;
    [SerializeField] private Animator _animator;
    public enum DestinationAnimation { idle, walk, run, sprint }
    private DestinationAnimation _destinationAnim;
    public enum TailAnimation { wag, wagFast, wagBroad }
    private TailAnimation _tailAnimation;

    public enum HeadAnimation { sniff, sniffGround, affection, disobediant, shame, PetHead }
    public HeadAnimation _headAnimation;

    //private string CurrentAnimationState;
    public Vector3 _petDestination = new Vector3();

    private IKFootBehavior _ikFootBehavior;
    private WaitForSeconds _WaitForDestinationChange = new WaitForSeconds(15f);
    [SerializeField]
    private PetInteractablesManager _petInteractionManager;

    //[SerializeField] private bool _updateRotation;

    public static bool isOKToFollowObject;
    [SerializeField] private float _interpoolationMultiplier = 0.75f;

    //movement variables
    private float _petsCurrentSpeed;
    //private float _movementWeight = .5f;
    private Vector3 lastPosition;
    IEnumerator _RandomDestination = null;

    //PickUpSequence Constraints
    public MultiAimConstraint multiAimConstraintNeck;
    private float _extendHeadForBall;
    public bool isOKToPickUpObject;
    [SerializeField] private float _pickupSequenceSpeed;

    public Transform followObjectTest;

    //might be able to use scriptable object instead
    //private const string _walk = "walk", _walkLeft = "walk_left", _walkRight = "walk_right", _run = "run", _runLeft = "run_left", _runRight = "run_right", _sprint = "sprint", _sprintLeft = "sprint_left",
    //    _sprintRight = "sprint_right", _tailWag = "tail_wag", _tailWagBroad = "tail_wag_broad", _tailWagFast = "tail_wag_fast", _tailSleep = "tail_sleep", _tailScared = "tail_scared", _chew = "chew",
    //    _chewOnObject = "chew_on_object", _closeMouth = "close_mouth", _yawn = "yawn", _headSniff = "head_sniff", _headShame = "head_shame";
    private void Awake()
    {
        _pet = gameObject.GetComponentInParent<NavMeshAgent>();
        _petTransform = gameObject.GetComponent<Transform>();
        _ikFootBehavior = gameObject.GetComponent<IKFootBehavior>();
        _parentTransform = GetComponentInParent<Transform>();
        _animator = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        _speed = (float)_destinationAnim;
        _RandomDestination = RandomDestination();
        //if (!_updateRotation)
        //{
        //    _pet.updateRotation = false;
        //}
    }
    private void Update()
    {
        //setting the characters feet and body rotation correctly to slopes and terrain
        _ikFootBehavior.RotateCharacterFeet();
        _ikFootBehavior.RotateCharactertBody();
        _ikFootBehavior.CharacterHeightAdjustment();

        //get the current speed of our pet     
        _petsCurrentSpeed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        //print("_petsCurrentSpeed: "+ _petsCurrentSpeed);

        lastPosition = transform.position;

        //controlls the animation states for movement blend tree
        VelocityInfluenceOnMovementBlendtree("Movement Weight");

        if (isOKToFollowObject)
        {
            //change navmesh agents destination to folow the object
            FollowObject(PetInteractablesManager.ActiveObjectOfInterest);
            PickupSequence(PetInteractablesManager.ActiveObjectOfInterest);
        }

    }
    private void OnEnable()
    {
        //whenever there is a state cahnge lets stop any potential roaming Random Destination coroutine state we might be in
        PetBehaviorStateMachine.changedState += StopRandomDestination;
        print("OnEnable Subscribed to StopRandomDestination");
    }
    private void OnDisable()
    {
        PetBehaviorStateMachine.changedState -= StopRandomDestination;
    }


    public void SetDestination(Transform destination)
    {
        //recalculates the navmesh pet/agent path to a updated destination position but does not move the agent
        SetDestinationPathCalculation(destination);

        //does not move pet/agent, just sets the _petDestination variable we need our pet/agent to go to, before we call _pet.destination
        _petDestination = destination.position;
        _pet.destination = _petDestination;
    }
    public void SetDestinationPathCalculation(Transform destination)
    {
        //calculates a new path for the navMesh agent, I must look into when it is usefull to call this function
        _pet.SetDestination(destination.position);
    }


    public void StartRandomDestinations()
    {
        StartCoroutine(_RandomDestination);
    }
    public IEnumerator RandomDestination()
    {
        //sets _petDestination to a random position and moves the pet to a random destination   
        while (true)
        {
            _petDestination = RandomDestinationPosition();
            _pet.destination = _petDestination;
            yield return _WaitForDestinationChange;
            print("setting another random destination for quadruped");
        }    
    }
    public void StopRandomDestination()
    {
        //if you whistle/ call the dogs name or other interuptable process
        StopCoroutine(_RandomDestination);
        _pet.destination = transform.position;
        print("Cancelled going to random destination");
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
     
    public void ChangeAnimationState(string animation, int layer=0)
    {
        _animator.Play(animation, 0, 0f);
        //_animator.Update(0f);
        _animator.CrossFade(animation, .15f, layer);
    }
    public void ChangeAnimationState(string animation)
    {
        //change the animation state of layer 1
        print("changing animation state to sit from wit voice command");

        _animator.Play(animation, 0, 0f);
        //_animator.Update(0f);
        _animator.CrossFade(animation, .15f, 0);
    }
    public void FollowObject(Transform obj)
    {
        float distance = Vector3.Distance(obj.position, transform.position);
        if (distance < .65f)
        {
            //we are close enough to the object, we can stop following it
            _pet.destination = transform.position;
        }
        else
        {
            //follow the object till the distance is close enough
            _pet.destination = obj.position;
        }
        PickupSequence(obj);
    }
    public void PickupSequence(Transform target)
    {
  

        //instead of coroutine, have it be distance based value changing as aproaching
        if(isOKToPickUpObject)
        {
            //If the object is within distance to pick up, then start the pickup sequence
            //print("Distance To Toy: "+ dist);

            float dist = Vector3.Distance(target.position, transform.position);
            if (dist < 1.5f)
            {
                StartCoroutine(PickupSequenceLerp());
            }
        }
      
    }

    IEnumerator PickupSequenceLerp()
    {
        float neckValue = 0f;
        float crouchValueStart = _animator.GetFloat("Movement Weight");
        float crouchValue = 0f;
        float time = 0f;
        while (time < 1)
        {
            //start neck lowering sequence
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
            //we now reverse the neck lowering sequence because we only played it to make contact with the jaw on the object to trigger a ICarryDropbable.Carry() 
            neckValue = Mathf.Lerp(1, 0, time);
            crouchValue = Mathf.Lerp(-1, 0, time);
            multiAimConstraintNeck.weight = neckValue;
            _animator.SetLayerWeight(1, neckValue);

            //tied into the movement blendtree is a pickUpObject animation node for the Movement Weight variable in the animator which we reverse out of along with MultiAimConstraint on the neck.
            _animator.SetFloat("Movement Weight", crouchValue);
            time += Time.deltaTime * _pickupSequenceSpeed;
            yield return null;
        }
    }

    private void VelocityInfluenceOnMovementBlendtree(string animatorParameter)
    {
        //the animation for idle, walk and run are blended based on a Movement Weight animation parameter that is set in Update() loop
        float MovementBlendSpeed = Mathf.InverseLerp(0, 6, _petsCurrentSpeed);
       // print("MovementBlendSpeed: " + MovementBlendSpeed);
        _animator.SetFloat(animatorParameter, MovementBlendSpeed);
        //if our angular speed is not near 0 then the movementblendspeed should be somewhere around .1 or so, so that when turning a corner the Quadruped is not in an idle animation rotating in place around corners
    } 
}
