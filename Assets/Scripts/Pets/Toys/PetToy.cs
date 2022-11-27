using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetToy : MonoBehaviour, IDataPersistence
{
    private int _rarity;
    private int _duribility;
    private string _flavor;
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

    public void SaveData(GameData data)
    {
        data.Rarity = _rarity;
        data.Duribility = _duribility;
        data.Flavor = _flavor;
    }
    public void LoadData(ref GameData data)
    {
        _rarity = data.Rarity;
        _duribility = data.Duribility;
        _flavor = data.Flavor;
    }
}
