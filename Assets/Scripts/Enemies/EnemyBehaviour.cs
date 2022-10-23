using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject enemyToChase;
    public GameObject[] patrolDestinations;
    NavMeshAgent meshAgent;
    private int waypointIndex;
    private float dist;
    public int speed;

    private int currentMode = 0;
    enum EnemyMode
    {
        Patrolling,
        Attacking,
        Alert,
        Defending
    }


    void Start()
    {
        waypointIndex = 0;
        transform.LookAt(patrolDestinations[waypointIndex].transform.position);
        meshAgent = GetComponent<NavMeshAgent>();
        meshAgent.SetDestination(patrolDestinations[waypointIndex].transform.position);
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");
        if (other.tag == "Squad")
        {
            Debug.Log("Enemy detected");
            currentMode = (int)EnemyMode.Attacking;
            enemyToChase = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        currentMode = (int)EnemyMode.Alert;
    }


    void Update()
    {
        switch (currentMode)
        {
            case (int)EnemyMode.Patrolling:
                dist = Vector3.Distance(transform.position, patrolDestinations[waypointIndex].transform.position);
                if (dist < 1f)
                {
                    IncreaseIndex();
                }
                break;

            case (int)EnemyMode.Attacking:
                meshAgent.SetDestination(enemyToChase.transform.position);

                break;

            case (int)EnemyMode.Alert:

                break;

            case (int)EnemyMode.Defending:


                break;

        }
    }


    void IncreaseIndex()
    {
        waypointIndex++;
        if(waypointIndex >= patrolDestinations.Length)
        {
            waypointIndex = 0;
        }
        transform.LookAt(patrolDestinations[waypointIndex].transform.position);
        meshAgent.SetDestination(patrolDestinations[waypointIndex].transform.position);
    }



    void ChaseEnemy()
    {
        meshAgent.SetDestination(enemyToChase.transform.position);
    }

}
