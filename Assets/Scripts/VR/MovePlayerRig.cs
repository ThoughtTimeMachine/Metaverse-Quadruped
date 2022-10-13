using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerRig : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float speed = 1;
    private bool isMoving = false;
    [SerializeField] private Transform hdm;
    void Start()
    {
        player = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            player.Translate(hdm.forward * speed * Time.deltaTime);
        }
    }

    public void Move()
    {
        isMoving = true;
    }
    public void StopMoving()
    {
        isMoving = false;
    }
}
