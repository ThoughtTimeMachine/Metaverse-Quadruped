using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetController : MonoBehaviour
{
    private int _random = 0;
    private float _speed;
    private NavMeshAgent _pet;
    public static Transform _petTransform;
    [SerializeField]
    private Animator animator;
    public enum DestinationAnimation { idle, walk, run, sprint }
    private DestinationAnimation _destinationAnim;
    public enum TailAnimation { wag, wagFast, wagBroad }
    private TailAnimation _tailAnimation;

    public enum HeadAnimation { sniff, sniffGround, affection, disobediant, shame, PetHead }
    public HeadAnimation _headAnimation;

    private string CurrentAnimationState;
    public Vector3 _petDestination = new Vector3();
    private WaitForSeconds _waitTime = new WaitForSeconds(2f);

    //subscripe StopRandomDestination to PetBehaviourSystem state changes with an observer pattern
    [SerializeField]
    private PetInteractablesManager _petInteractionManager;

    private void Awake()
    {
        _pet = gameObject.GetComponent<NavMeshAgent>();
        _petTransform = gameObject.GetComponent<Transform>();
    }
    //might be able to use scriptable object instead
    private const string _walk = "walk", _walkLeft = "walk_left", _walkRight = "walk_right", _run = "run", _runLeft = "run_left", _runRight = "run_right", _sprint = "sprint", _sprintLeft = "sprint_left",
        _sprintRight = "sprint_right", _tailWag = "tail_wag", _tailWagBroad = "tail_wag_broad", _tailWagFast = "tail_wag_fast", _tailSleep = "tail_sleep", _tailScared = "tail_scared", _chew = "chew",
        _chewOnObject = "chew_on_object", _closeMouth = "close_mouth", _yawn = "yawn", _headSniff = "head_sniff", _headShame = "head_shame";
    private void Start()
    {
        _speed = (float)_destinationAnim;
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
            animator.CrossFade(animation, .25f, layer);
        }
    }
    public void CatchObject(Transform obj)
    {
        //if standing/sitting in place rotate head to target and chomp when at correct distance
        //if sitting and objects at distance above our jump path:
        //changeAnimationState to jump(have min and max range for jump height)
        //rotate body and 
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
