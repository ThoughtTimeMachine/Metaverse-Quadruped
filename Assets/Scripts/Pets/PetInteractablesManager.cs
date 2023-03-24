using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;

public class PetInteractablesManager : Singleton<PetInteractablesManager>
{
    //store the in scene interactable objects in a dictionary for reference
    public static readonly Dictionary<int, Transform> OtherInteractables = new Dictionary<int, Transform>();
    public static readonly Dictionary<int, Transform> Foods = new Dictionary<int, Transform>();
    public static readonly Dictionary<string, Transform> Toys = new Dictionary<string, Transform>();

    public List<Transform> StaticObjectsOfCuriosity = new List<Transform>();


    public Transform Pet; // { get; private set; }

    public Transform Waterdish { get; private set; }
    public Transform FoodDish { get; private set; }

    //an extra variable to hold the dictionary index of the active toy or food.
    public int? ActiveToyIndex;//unassign after exiting interaction with object(SetTargetToNearestToy sets this to the nearest object when called)
    public int? ActiveFoodIndex;//unassign after exiting interaction with object(SetTargetToNearestFood sets this to the nearest object when called)

    //this is the current object of interest. ANYTIME the quadraped is going to interact with an interactable object in the scene, it should be set as the ActiveObjectOfInterest here!!
    public static Transform ActiveObjectOfInterest { get; private set; }

    [SerializeField] private Transform _toyInstantiationPosition;
    [SerializeField] private Transform _toyInstantiationParent;
    private void Awake()
    {
        Waterdish = GameObject.FindGameObjectWithTag("WaterBowl").transform;
        FoodDish = GameObject.FindGameObjectWithTag("FoodBowl").transform;
        Pet = GameObject.FindGameObjectWithTag("Pet").transform;
    }
    public void CreateToy(GameObject myPrefab)
    {
        //instatiate a Toy object from our inventory and parent it to the InteractableObjectContainer in the scene
        GameObject petToy;
        petToy = Instantiate(myPrefab, _toyInstantiationParent);
        petToy.transform.localPosition = Vector3.zero;

        //add to Toys dictionary of currently active toys in scene
        Toys.Add(myPrefab.name, petToy.transform);

        //set the ActiveObjectOfInterest to the new toy we initialized
        ActiveObjectOfInterest = petToy.transform;
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
                ActiveFoodIndex = i;
                ActiveObjectOfInterest = Foods[i];
            }
        }
    }
    public void SetTargetToNearestToy()
    {
        if (Toys.Count > 0)
        {
            var shortestDistance = Mathf.Infinity;
            for (var i = 0; i < Toys.Count; i++)
            {

                var distance = Vector3.Distance(Pet.position, Toys.ElementAt(i).Value.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    ActiveToyIndex = i;
                    ActiveObjectOfInterest = Toys.ElementAt(i).Value;
                    print("Nearest Toy: " + Toys.ElementAt(i).Key);
                }
                else
                {
                    print("Nearest Toy distance > shortestDistance");
                }
            }
        }

    }
}
