using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PetStateIdle : PetBehaviorState
{
    
    private float speed;

    [SerializeField]
    private PetInteractablesManager _petInteractablesManager;
  
    public PetStateIdle(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {
    }
    private void Update()
    {
        
    }
    public override void Start()
    {
        RandomIdleDestination();// Invoke reapeating with ability to stop. What determins the length of time when reaching the random destination and actions performed like sniff?
        //you can set the state of the petBehaviourSystem to another state here
        //start idle and casually walk around the environment checking things out untill PetCarBars deplete or increase changing the state

    }

    private void RandomIdleDestination()
    {
        int randomIdleDestination = Random.Range(0, _petInteractablesManager.StaticObjectsOfCuriosity.Count - 1);
        PetBehaviorSystem._petController.GoToDestination(_petInteractablesManager.StaticObjectsOfCuriosity[randomIdleDestination].position);
    }
  
}
