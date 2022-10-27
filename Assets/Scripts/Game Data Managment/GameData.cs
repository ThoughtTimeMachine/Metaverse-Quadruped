using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int rarity;
    public int duribility;
    public string flavor;
    public SerializableDictionary<string, bool> FoodsInEnvironment;

    public GameData()
    {
        this.rarity = 0;
        this.duribility = 0;
        this.flavor = "Chicken";
        FoodsInEnvironment = new SerializableDictionary<string, bool>();
    }
}
