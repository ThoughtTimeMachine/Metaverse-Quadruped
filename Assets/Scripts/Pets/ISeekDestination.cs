using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISeekDestination 
{
    void GoToDestination(Transform transform, Vector3 destination);//interface parameter that can supply position of object or palce to be at
}
