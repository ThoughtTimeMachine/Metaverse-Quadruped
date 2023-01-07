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

    [SerializeField] private ParentConstraint _parentConstraint;
    private ConstraintSource _source;
    private Transform _toyObjectHolderParent;
    private void OnEnable()
    {

    }
    void Start()
    {
        _toyObjectHolderParent = gameObject.transform.parent.transform;
        _parentConstraint = gameObject.GetComponent<ParentConstraint>();
        AddSourceToJawBoneConstraint();
    }

    public void AddSourceToJawBoneConstraint()
    {
        _source.weight = 1;
        _source.sourceTransform = GameObject.FindGameObjectWithTag("ToyTargetJaw").transform;
        _parentConstraint.AddSource(_source);
    }
    public void Catch()
    {
        transform.parent = null;//detatch the child object from the parent so that the dogs head is still pointed at the ball and we can lerp the head and neck back to the player interacting and throwing the balls direction. re use in an small object pool type set up.
        _parentConstraint.weight = 1;//Maybe lerp value quickly?
        //var sources = _constraint.data.sourceObjects;
        //sources.SetWeight(0, 0.0f);
        //_constraint.data.sourceObjects = sources;
    }
    public void Drop()
    {
        _parentConstraint.weight = 0;
        //re position toyObjectHolder to this.gameobjects.transform.position, reparent this.gameobjects.transform.position to toyObjectHolder
        _toyObjectHolderParent.position = this.gameObject.transform.position;
        this.gameObject.transform.parent = _toyObjectHolderParent;

        //unparent if needed or re parent to orgional object we unparentd from so that neck and head could still face object direction on intercept
    }
    public void Chew()
    {
        //implement mesh deformation here or wearing out of toy through texture or retunring to object pool system if eating
    }
    public void Carry()
    {
        //not sure if needed yet. Think through if any senario needs something to be held through a method
        //set our parentConstriant.weight to 1 to be carried int eh pets mouth
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
