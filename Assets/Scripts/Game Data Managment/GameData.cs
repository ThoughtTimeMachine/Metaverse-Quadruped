using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int Rarity;
    public int Duribility;
    public string Flavor;
    public SerializableDictionary<string, bool> FoodsInEnvironment;

    //PetStatusBars
    public float Happiness;
    public float Hunger;
    public float Thirst;
    public float Boredom; //the longer in boredom, happiness slowly goes down
    public float Bathroom;
    public float Energy;
    public float Cleanliness;
    public float LastSavedTime;

    public GameData()
    {
        Rarity = 0;
        Duribility = 0;
        Flavor = "Chicken";
        FoodsInEnvironment = new SerializableDictionary<string, bool>();
    }
}
