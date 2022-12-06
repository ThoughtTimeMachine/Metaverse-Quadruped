using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetController : MonoBehaviour
{
    private float _speed;
    private NavMeshAgent _pet;
    public static Transform _petTransform;
    public enum DestinationAnimation { idle, walk, run, sprint }
    private DestinationAnimation _destinationAnim;
    public enum TailAnimation { wag, wagFast, wagBroad }
    private TailAnimation _tailAnimation;

    public enum HeadAnimation {sniff, sniffGround, affection, disobediant, shame, PetHead }
    public HeadAnimation _headAnimation;

    private void Awake()
    {
        _pet = gameObject.GetComponent<NavMeshAgent>();
        _petTransform = gameObject.GetComponent<Transform>();
    }

    private void Update()
    {

    }
    private void Start()
    {
        _speed = (float)_destinationAnim;
        //you can set the state of the petBehaviourSystem to another state here
        //start idle and casually walk around the environment checking things out untill PetCarBars deplete or increase changing the state

    }

    //subscribe or get call from the petBehviorSystem delegate call on a state change in that class. We either disable our state or enable it?

    public void GoToDestination(Vector3 destination)//interface parameter that can supply position of object or palce to be at
    {
        _pet.SetDestination(destination);
        //petTransform.position = Vector3.MoveTowards(petTransform.forward, destination, speed * Time.deltaTime);

    }

    private void HeadMovement()
    {

    }
}
