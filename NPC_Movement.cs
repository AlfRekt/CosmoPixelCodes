using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Movement : MonoBehaviour
{
    public NavMeshAgent _agent;
    public Vector3 _destinationPoint;
    private bool destinationPointIsSet;
    public float maxX;
    public float minX;
    public float minY;
    public float maxY;
    public LayerMask ground;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Patroling();
    }

    void Patroling()
    {
        if (!destinationPointIsSet)
        {
            SearchWalkPoint();
        }

        if (destinationPointIsSet)
        {
            _agent.SetDestination(_destinationPoint);
        }

        Vector3 distanceToDestinationPoint = transform.position - _destinationPoint;
        if(distanceToDestinationPoint.magnitude < 1.0f)
        {
            destinationPointIsSet = false;
        }
    }

    void SearchWalkPoint()
    {
        float _randomX = Random.Range(minX, maxX);
        float _randomY = Random.Range(minY, maxY);

        _destinationPoint = new Vector3( transform.position.x + _randomX, transform.position.y + _randomY, 0 );

        if(Physics2D.Raycast(_destinationPoint, -transform.up,2.0f,ground))
        {
            destinationPointIsSet = true;
        }
    }
}

