using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetToy : MonoBehaviour, IDataPersistence
{
    private int rarity;
    private int duribility;
    private string flavor;
    //public delegate void
    private void OnEnable()
    {

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadData(GameData data)
    {
        data.rarity = this.rarity;
        data.duribility = this.duribility;
        data.flavor = this.flavor;
    }
    public void SaveData(ref GameData data)
    {
        this.rarity = data.rarity;
        this.duribility = data.duribility;
        this.flavor = data.flavor;
    }
}
