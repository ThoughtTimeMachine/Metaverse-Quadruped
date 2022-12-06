using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetInteractablesManager : Singleton<PetInteractablesManager>
{
    //The int key is 0 = toy, 1 = food, 2 water, 3 other interactable non destructive, 4 other interactable destructive
    public static readonly Dictionary<int, Transform> OtherInteractables = new Dictionary<int, Transform>();
    public static readonly Dictionary<int, Transform> Foods = new Dictionary<int, Transform>();
    public static readonly Dictionary<int, Transform> Toys = new Dictionary<int, Transform>();

    public List<Transform> StaticObjectsOfCuriosity = new List<Transform>();


    public Transform Pet { get; private set; }
    public Transform Waterdish { get; private set; }
    public Transform FoodDish { get; private set; }

    public int? ActiveToy;//unassign after exiting interaction with object
    public int? ActiveFood;//unassign after exiting interaction with object
    public Transform ActiveObjectOfInterest { get; private set; }
    public void CreateToy()
    {
        //create object/ instantiate prefab, add components needed if any, object pool system if many can be used or created.
    }
    public void CreateFood()
    {
        //create object/ instantiate prefab, add components needed if any then add to list 
    }
    public float GetDistanceToNearestFood()
    {
        var shortestDistance = Mathf.Infinity;
        for (var i = 0; i < Foods.Count; i++)
        {
            var distance = Vector3.Distance(Pet.position, Foods[i].position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                return distance;
            }
        }
        return 0f;
    }
    public float GetDistanceToNearestToy()
    {
        var shortestDistance = Mathf.Infinity;
        for (var i = 0; i < Toys.Count; i++)
        {
            var distance = Vector3.Distance(Pet.position, Toys[i].position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                return distance;
            }
        }
        return 0f;
    }
    public void SetTargetToNearestFood()
    {
        var shortestDistance = Mathf.Infinity;
        for (var i = 0; i < Foods.Count; i++)
        {
            var distance = Vector3.Distance(Pet.position, Foods[i].position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                ActiveFood = i;
                ActiveObjectOfInterest = Foods[i];
            }
        }
    }
    public void SetTargetToNearestToy()
    {
        var shortestDistance = Mathf.Infinity;
        for (var i = 0; i < Toys.Count; i++)
        {
            var distance = Vector3.Distance(Pet.position, Toys[i].position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                ActiveFood = i;
                ActiveObjectOfInterest = Toys[i];
            }
        }
    }
}
