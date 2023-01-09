using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationHelpers : MonoBehaviour
{
    
    private void Update()
    {
        Debug.Log("Y-axis rotation rate: " + AngleOfRotation() + " degrees per second");
    }
    // Extract the angle of rotation from the Quaternion
    private float AngleOfRotation()
    {
        float elapsedTime = Time.deltaTime;
        float angle;
        Vector3 axis;
        Quaternion rot = transform.rotation;

        rot.ToAngleAxis(out angle, out axis);
        float rotRate = angle / elapsedTime;
        return rotRate;
    }


    // Calculate the rate of change of the y-axis rotation


}
