using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;

public class PetInteractablesManager : Singleton<PetInteractablesManager>
{
    //The int key is 0 = toy, 1 = food, 2 water, 3 other interactable non destructive, 4 other interactable destructive
    public static readonly Dictionary<int, Transform> OtherInteractables = new Dictionary<int, Transform>();
    public static readonly Dictionary<int, Transform> Foods = new Dictionary<int, Transform>();
    public static readonly Dictionary<string, Transform> Toys = new Dictionary<string, Transform>();
    public List<Transform> StaticObjectsOfCuriosity = new List<Transform>();


    public Transform Pet { get; private set; }
    public Transform Waterdish { get; private set; }
    public Transform FoodDish { get; private set; }

    public int? ActiveToy;//unassign after exiting interaction with object
    public int? ActiveFood;//unassign after exiting interaction with object
    public Transform ActiveObjectOfInterest { get; private set; }

    [SerializeField] private Transform _toyInstantiationPosition;
    private void Awake()
    {
        Waterdish = GameObject.FindGameObjectWithTag("WaterBowl").transform;
        FoodDish = GameObject.FindGameObjectWithTag("FoodBowl").transform;
        Pet = GameObject.FindGameObjectWithTag("Pet").transform;
    }
    public void CreateToy(GameObject myPrefab)
    {
        GameObject petToy;
        petToy = Instantiate(myPrefab, _toyInstantiationPosition.position, Quaternion.identity);
        Toys.Add(myPrefab.name, petToy.transform);          
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
            var distance = Vector3.Distance(Pet.position, Toys.ElementAt(i).Value.position);
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
          
            var distance = Vector3.Distance(Pet.position, Toys.ElementAt(i).Value.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                ActiveFood = i;
                ActiveObjectOfInterest = Toys.ElementAt(i).Value;
            }
        }
    }
}
