using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetCareBars : MonoBehaviour
{
    private float _happiness;
    private float _hungry;
    private float _thirsty; 
    private float _bored; //the longer in boredom, happiness slowly goes down
    private float _bathroom; 
    private float _tired;
    private float _cleaness;
    protected enum SpeedAnimation { idle, walk, run, sprint }
    protected SpeedAnimation speedAnim;
    protected enum CurrentState { idle, tired, thirsty, sick, playfull, hungry, curious }
    protected CurrentState currentState;

    private void Update()
    {
        
    }
    private void OnEnable()
    {
        
    }
    void Start()
    {
        
    }

    private void Happiness(float value)
    { }
    private void Hungry()
    { }
    private void Thirsty()
    { }
    private void Bored()
    { }
    private void Bathroom()
    { }
    private void Tired()
    { }
    private void Cleaness()
    { }
}
