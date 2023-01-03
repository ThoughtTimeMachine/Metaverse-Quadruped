using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    //could reference a scriptable object for the string const
    private string _animation;
    private int _animationLayer;
    [SerializeField]private string ObjectCollidedWithsTag;
    public UnityEvent _MyEventEnter;
  //  public UnityEvent _MyEventExit;
    [SerializeField] private MultiAimConstraint _constraint;
    [SerializeField] private Transform _child;
    [SerializeField] private GameObject _ToyToActivateOnCatch;
    public void OnTriggerEnter(Collider objectCollidedWith)
    {
        if (objectCollidedWith.tag == ObjectCollidedWithsTag)
        {
            Interact(objectCollidedWith);
            _child.parent = null;
            _ToyToActivateOnCatch.SetActive(true);
            objectCollidedWith.gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Invoke("callFunction", 2f) ;
    }
    private void Interact(Collider Obj)
    {
        //if (Obj.GetComponent<PetBehaviorSystem>()._currentState == PetBehaviorSystem.CurrentState.hungry)
        //{
        //    Obj.GetComponent<PetController>().ChangeAnimationState(_animation, _animationLayer);//might add another function in the pet class and set a transform value for IK to proceduraly set the face, mouth and head to the target.
        //}
        
        //_constraint.weight = 0.79f;
        //_MyEventEnter.Invoke();
    }
    //private void callFunction()
    //{
    //    _MyEventExit.Invoke();
    //}
}
