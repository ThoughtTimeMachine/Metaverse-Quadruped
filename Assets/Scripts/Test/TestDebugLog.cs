using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDebugLog : MonoBehaviour
{
     public float LengthOfTime = 172800; //seconds in 48 hours
     private float _currentTime;
     private float _timeDivdedByLengthOfTime;
    public GameObject CubeVisual;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // _currentTime += Time.deltaTime;

        _timeDivdedByLengthOfTime += Time.deltaTime / LengthOfTime;
        if(_timeDivdedByLengthOfTime >= 1f)
        {
            CubeVisual.SetActive(true);
        }

    
    }
}
