using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;

public class InteractableTrigger : MonoBehaviour
{
    //could reference a scriptable object for the string const
    //this needs to be generic or some other pattern needs to be done so this is more generically usefull if possible
    [SerializeField]private string ObjectCollidedWithsTag;
    public void OnTriggerEnter(Collider objectCollidedWith)
    {
        if (objectCollidedWith.CompareTag(ObjectCollidedWithsTag))
        {
            objectCollidedWith.transform.GetChild(0).GetComponent<IToy>().Carry();
        }
    }
  
}
