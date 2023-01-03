using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class PetToy : MonoBehaviour, IDataPersistence
{
    private int _rarity;
    private int _duribility;
    private string _flavor;
    //public delegate void
   
    [SerializeField] private Rigidbody rb;
    [SerializeField]  private Animator _animator;
    private void OnEnable()
    {

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _animator.GetFloat("RF foot weight");
    }
    private void OpenMouthForObj()
    {
        _animator.GetFloat("RF foot weight");
    }
    public void CatchGameobject(Transform transf)
    {
        rb.useGravity = false;
        rb.isKinematic = true;
       // print("Caught The game Object");
       

        //var sources = _constraint.data.sourceObjects;
        //sources.SetWeight(0, 0.0f);
        //_constraint.data.sourceObjects = sources;

       
       // transform.SetParent(transf);
        //transform.position = transf.position;
        transform.localPosition = Vector3.zero;
        //Invoke("SetPosition", 0.5f);
    }
    private void SetPosition()
    {
        transform.localPosition = Vector3.zero;
      //  print("setPosition");
    }
    public void ReleaseGameObject()
    {
        transform.SetParent(null);
        rb.useGravity = true;
        rb.isKinematic = false;
      //  print("ReleaseObject");
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
