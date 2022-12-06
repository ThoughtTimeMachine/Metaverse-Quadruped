using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PetVision : MonoBehaviour
{
    private Transform _pet;
    private Vector3 _halfBoxSize = new Vector3(.5f,.5f,.5f);
    private float _maxDistance = 20f;
    private RaycastHit[] _hits;
    public Transform ObjectToFollow; 

    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //look for object
        //rotate neck bone and move through a coroutine while foundBool == false
    }

    public void LookForObject(int objIDToFind, int ObjLayer)
    {
        _hits = Physics.BoxCastAll(transform.position, _halfBoxSize, Vector3.forward, transform.rotation, _maxDistance, 6);

        if(_hits != null)
        {
            foreach(RaycastHit hit in _hits )
            {
                if(hit.collider.GetComponent<ObjectID>().ID == objIDToFind)
                {
                    ObjectToFollow = hit.transform;
                }
            }
        }
    }

    public void UnfollowObject()
    {
        ObjectToFollow = null;
    }
    private void RotateNeckBone()
    {
        // Speed and rotation degree based on enum
    }
}
