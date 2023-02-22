using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public Camera cam;
    public Animator animator;
    private string _currentAnimationState = "Idle A";
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = cam.transform;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            print("World Point Ray Hit: " + ray.direction);
            if (Physics.Raycast(ray, out hit))
            {
                print("Moving NavmeshAgent to : " + hit.point);
                agent.destination = hit.point;
            }
        }
        CheckIfMoving();
    }

    public void CheckIfMoving()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    ChangeAnimationState("Walk",0);

                }
                else
                {
                    ChangeAnimationState("Idle A",0);
                }
            }
        }
    }
        public void ChangeAnimationState(string animation, int layer)
        {
            if (_currentAnimationState != animation)
            {
            _currentAnimationState = animation;
            animator.CrossFade(animation, .25f, layer);
            }
        }
    }
