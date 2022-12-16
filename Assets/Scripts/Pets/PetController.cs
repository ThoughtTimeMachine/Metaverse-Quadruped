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

    [SerializeField]
    private PetInteractablesManager _petInteractionManager;
    private void Awake()
    {
        _pet = gameObject.GetComponent<NavMeshAgent>();
        _petTransform = gameObject.GetComponent<Transform>();
    }
    private const string _walk = "walk", _walkLeft = "walk_left", _walkRight = "walk_right", _run = "run", _runLeft = "run_left", _runRight = "run_right", _sprint = "sprint", _sprintLeft = "sprint_left", 
        _sprintRight = "sprint_right",_tailWag = "tail_wag", _tailWagBroad = "tail_wag_broad", _tailWagFast = "tail_wag_fast", _tailSleep = "tail_sleep", _tailScared = "tail_scared", _chew = "chew",
        _chewOnObject = "chew_on_object", _closeMouth = "close_mouth",_yawn = "yawn", _headSniff = "head_sniff", _headShame = "head_shame";
    private void Start()
    {
        _speed = (float)_destinationAnim;
    }

    //subscribe or get call from the petBehviorSystem delegate call on a state change in that class. We either disable our state or enable it
    public void SetDestination(Transform destinations)
    {
        _petDestination = destinations.position;
    }
    IEnumerator GoToDestination()
    {
        while (Mathf.Approximately(_pet.remainingDistance, 0f))
        {
            _pet.destination = _petDestination;
            yield return null;
        }
    }

    public void NextDestinationTest()
    {
        InvokeRepeating("StartRandomDestination", 0f,24f);
    }
    public void StartRandomDestination()
    {
        _petDestination = RandomDestinationPosition();
        _pet.destination = _petDestination;
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
    public void ChangeAnimationState(string animation,int layer)
    {
        if (CurrentAnimationState != animation)
        {
            animator.CrossFade(animation,.25f,layer);
        }
    }
    private void HeadMovement()
    {

    }
}
