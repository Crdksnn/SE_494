using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class PlayerMapMovementControl : MonoBehaviour
{
    [Header("CAMERA")]
    [SerializeField] Camera cam;

    [Header("SCENE FADER")] 
    [SerializeField] SceneFader sceneFader;
    
    [Header("PLAYER MOVEMENT CONFIGURES")]
    [SerializeField] NavMeshAgent agent;
    private Vector3 _targetPosition;
    private bool _isReachedToCity;
    
    void Start()
    {

       

    }

    private void Update()
    {
        
        SetTargetPointAndClick();
        ControlReachToTrader();
    }
    
    //Sending ray from camera to ground when player pressed mouese button 1
    private void SetTargetPointAndClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray targetPos = cam.ScreenPointToRay(Input.mousePosition);
            
            //Checking ray collide with something or not
            if (Physics.Raycast(targetPos, out var hit))
            {
                //If collide with city, player will move to city enter position
                if (hit.transform.CompareTag("City"))
                {
                    _isReachedToCity = true;
                    _targetPosition = hit.transform.GetComponent<City>().cityEnterPoint.position;
                    agent.SetDestination(_targetPosition);
                    return;
                }
                
                //If collide with path object, player will move to target position
                _targetPosition = hit.point;
                agent.SetDestination(_targetPosition);
                _isReachedToCity = false;
            }

        }
    }

    //Checking player reach to target or not
    void ControlReachToTrader()
    {
        if (_isReachedToCity)
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        sceneFader.FadeTo("City Scene");
                        _isReachedToCity = false;
                    }
                }
            }
        }

    }
    
}