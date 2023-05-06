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

    float timer;
    [SerializeField] float attackTimer = 1.0f;
    [SerializeField] int attackDamage = 20;
    bool initialAttack;
    float initAttackTimer;




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
        initAttackTimer = (float)(attackTimer * 0.2);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Squad" || other.tag == "Player")
        {
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
                Patrol();
                break;

            case (int)EnemyMode.Attacking:
                ChaseEnemy();
                break;

            case (int)EnemyMode.Alert:
                enemyToChase = null;
                break;

            case (int)EnemyMode.Defending:
                enemyToChase = null;
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

    void Patrol()
    {
        enemyToChase = null;
        dist = Vector3.Distance(transform.position, patrolDestinations[waypointIndex].transform.position);
        if (dist < 1f)
        {
            IncreaseIndex();
        }
    }


    void ChaseEnemy()
    {
        if (meshAgent.remainingDistance >= 1.0f)
        {
            meshAgent.isStopped = false;
            meshAgent.SetDestination(enemyToChase.transform.position);
        }

        if (meshAgent.remainingDistance < 2.0f)
        {
            meshAgent.isStopped = true;
            Attack();
        }
    }

    void Attack()
    {
        Debug.Log("Attacking");
        if (initialAttack)
        {
            initAttackTimer += Time.deltaTime;
            if (initAttackTimer >= attackTimer)
            {
                enemyToChase.GetComponent<Health>().TakeDamage(attackDamage);
                initialAttack = false;
                timer = 0;
            }
        }

        timer += Time.deltaTime;
        if (timer >= attackTimer)
        {
            enemyToChase.GetComponent<Health>().TakeDamage(attackDamage);
            timer = 0;
        }
    }


    void Alert()
    {

    }

    void Defending()
    {

    }

}
