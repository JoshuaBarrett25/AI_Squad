using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerFollow : MonoBehaviour
{
    public Transform targetTransform;
    NavMeshAgent navAgent;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        navAgent.SetDestination(targetTransform.position);
    }
}
