using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetInteractablesManager : Singleton<PetInteractablesManager>
{
    //The int key is 0 = toy, 1 = food, 2 water, 3 other interactable non destructive, 4 other interactable destructive
    public static readonly Dictionary<int,Transform> Interactables = new Dictionary<int,Transform>();
    private Transform _pet;
   
    public void CreateToy()
    {
        //create object/ instantiate prefab, add components needed if any
    }
    public void CreateFood()
    {
        //create object/ instantiate prefab, add components needed if any then add to list 
    }

}
