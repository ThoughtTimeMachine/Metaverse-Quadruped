using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetVision : MonoBehaviour
{
    private Transform _pet;
    private Vector3 _halfBoxSize = new Vector3(.5f,.5f,.5f);
    private float _maxDistance = 20f;
    private RaycastHit[] _hits;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //look for object
        //rotate neck bone and move through a coroutine while foundBool == false
    }

    public void LookForObject(int objIDToFind, int ObjLayer)//interface parameter that can supply position of object or palce to be at
    {
        _hits = Physics.BoxCastAll(transform.position, _halfBoxSize, Vector3.forward, transform.rotation, _maxDistance, 6);

        if(_hits != null)
        {
            foreach(RaycastHit hit in _hits )
            {
                if(hit.collider.GetComponent<ObjectID>().ID == objIDToFind)
                {
                    //we found the fawking object bro, lets interact with it
                }
            }
        }

        //var ray = Physics.Raycast(tr)
        //begin raycasting from pet
        //if the raycast hits a objLayer and it is the objID th pets looking for, w have found the object
    }

    private void RotateNeckBone()
    {
        //
    }
}
