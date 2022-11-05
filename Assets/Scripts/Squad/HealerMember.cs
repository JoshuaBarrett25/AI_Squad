using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealerMember : MonoBehaviour
{
    GameObject player;
    NavMeshAgent navagent;
    public static int currentMode;
    static public int orderSelected;
    Transform enemytochase;


    bool previousFull;
    GameObject previousHitCover;
    GameObject hitCover;
    CoverScript coverscript;

    static public bool pointToGo;
    public Transform pointLocation;
    public Camera fpsCamera;
    public LayerMask groundLayer;
    public LayerMask defendLayer;

    bool cooldownactive;
    float timer;
    float squadCooldown = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemytochase = other.transform;
        }    
    }

    public enum AimingMode
    {
        Shoot,
        Squad
    }

    public enum memberOrders
    {
        Stop,
        Follow,
        Move,
        Defend,
        Chasing,
        SQUADMODE
    }


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navagent = gameObject.GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        if (Input.GetButton("Switch"))
        {
            currentMode = (int)AimingMode.Squad;
        }

        if (Input.GetButton("Cancel"))
        {
            RadialMenu.isMemberSelected = false;
            RadialMenu.isActionSelected = false;
            currentMode = (int)AimingMode.Shoot;
        }


        if (RadialMenu.squadselected == (int)RadialMenu.memberSelected.Healer)
        {
            orderSelected = RadialMenu.orderselected;
        }



        switch (orderSelected)
        {
            case (int)memberOrders.Stop:
                Stop();
                break;

            case (int)memberOrders.Follow:
                Follow();
                break;

            case (int)memberOrders.Move:
                Move();
                break;

            case (int)memberOrders.Defend:
                if (!pointToGo)
                {
                    Point();
                }
                if (pointToGo)
                {
                    DefendCast();
                }
                break;
        }
    }


    void Follow()
    {
        navagent.isStopped = false;
        navagent.SetDestination(player.transform.position);
    }

    void Stop()
    {
        navagent.isStopped = true;
    }


    void Attack()
    {

    }


    void ChaseEnemy()
    {
        navagent.SetDestination(enemytochase.position);

        if (navagent.remainingDistance < 1f)
        {
            Attack();
        }
    }



    void Move()
    {
        navagent.isStopped = false;
        AimCast();
        navagent.SetDestination(pointLocation.position);
        //navagent.SetDestination();
    }

    void AimCast()
    {
        navagent.isStopped = false;
        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.TransformDirection(Vector3.forward), out hit, 30, groundLayer.value))
        {
            if (Input.GetButton("Fire") && !cooldownactive)
            {
                Debug.Log("Hitting ground");
                pointLocation = hit.transform;
                cooldownactive = true;
            }
        }
    }

    void Point()
    {
        if (Input.GetButton("Fire") && !cooldownactive && !pointToGo)
        {
            pointToGo = true;
            cooldownactive = true;
            Debug.Log("SDAJSD");
        }
    }

    void DefendCast()
    {
        navagent.isStopped = false;
        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.TransformDirection(Vector3.forward), out hit, 30, defendLayer.value))
        {
            hitCover = hit.collider.gameObject;
            if (hitCover != previousHitCover && previousFull)
            {
                previousHitCover.GetComponent<CoverScript>().hitmaterial = false;
            }
            coverscript = hitCover.GetComponent<CoverScript>();
            coverscript.hitmaterial = true;
            if (Input.GetButton("Fire"))
            {
                navagent.SetDestination(pointLocation.position);
                Debug.Log("Healer moving to cover");
                pointLocation = hit.transform;
                cooldownactive = true;
            }
            previousHitCover = hitCover;
            previousFull = true;
        }

        else
        {
            previousHitCover.GetComponent<CoverScript>().hitmaterial = false;
            coverscript.hitmaterial = false;
        }
    }
}
