using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    //could reference a scriptable object for the string const
    private string _animation;
    private int _animationLayer;
    public void OnTriggerEnter(Collider objectCollidedWith)
    {
        if (objectCollidedWith.tag == "Pet")
        {
            Interact(objectCollidedWith);
       
        }
    }

    private void Interact(Collider Obj)
    {
        if (Obj.GetComponent<PetBehaviorSystem>()._currentState == PetBehaviorSystem.CurrentState.hungry)
        {
            Obj.GetComponent<PetController>().ChangeAnimationState(_animation, _animationLayer);//might add another function in the pet class and set a transform value for IK to proceduraly set the face, mouth and head to the target.
        }
    }
}
