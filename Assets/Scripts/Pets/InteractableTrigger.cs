using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;

public class InteractableTrigger : MonoBehaviour
{
    //could reference a scriptable object for the string const
    [SerializeField]private string ObjectCollidedWithsTag;
    public UnityEvent _MyEventEnter;

    public void OnTriggerEnter(Collider objectCollidedWith)
    {
        if (objectCollidedWith.CompareTag(ObjectCollidedWithsTag))
        {
            _MyEventEnter.Invoke();          
        }
    }
  
}
