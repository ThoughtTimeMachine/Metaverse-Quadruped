using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float m_Thrust = 20f;
    private Vector3 savedStartingPosition;
    private Quaternion savedStartingRotation;
    void Start()
    {
        savedStartingPosition = transform.position;
        savedStartingRotation = transform.rotation;
        //Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Jump"))
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            m_Rigidbody.AddForce(transform.forward * m_Thrust);
        }
        if (Input.GetButton("Fire2"))
        {
           transform.position = savedStartingPosition;
           transform.rotation = savedStartingRotation;
        }
    }
}

