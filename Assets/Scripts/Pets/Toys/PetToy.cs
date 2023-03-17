using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

public class PetToy : MonoBehaviour, IDataPersistence, IToy
{
    [SerializeField] private int _rarity;
    [SerializeField] private int _duribility;
    [SerializeField] private string _flavor;


    private Transform _QuadrapedJawboneParent;
    private void OnEnable()
    {

    }
    void Start()
    {
        _QuadrapedJawboneParent = GameObject.FindGameObjectWithTag("JawTarget").transform;
    }

    public void Catch()
    {

    }
    public void Drop()
    {   //unparent toy from jaw and re enable gravity
        transform.parent.SetParent(null);
        transform.parent.GetComponent<Rigidbody>().useGravity = true;
        transform.parent.GetComponent<Rigidbody>().isKinematic = false;
    }
    public void Chew()
    {
        //implement mesh deformation here or wearing out of toy through texture or retunring to object pool system if eating
    }
    public void Carry()
    {
        //parent this toy to the jaw of the quadraped and disable gravity on toy while parented to Quadrapeds Jaw
        transform.parent.GetComponent<Rigidbody>().useGravity = false;
        transform.parent.GetComponent<Rigidbody>().isKinematic = true;

        transform.parent.SetParent(_QuadrapedJawboneParent);
        transform.parent.localPosition = Vector3.zero;
        transform.localPosition = Vector3.zero;
        PetController.isOKToFollowObject = false;
        transform.GetComponentInParent<PetController>().isOKToPickUpObject = false;
        //could stop coroutine for pick up animations as soon as toy is in mouth, as to stop the continued downward or upward movement of the neck constraint
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
