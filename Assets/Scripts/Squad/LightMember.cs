using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LightMember : MonoBehaviour
{
    public GameObject playerLocation;
    NavMeshAgent navagent;
    static public int currentMode;
    static public int orderSelected;

    bool previousFull;
    GameObject previousHitCover;
    GameObject hitCover;
    CoverScript coverscript;

    public Transform pointLocation;
    static public bool pointToGo;
    public Camera fpsCamera;
    public LayerMask groundLayer;
    public LayerMask defendLayer;

    bool cooldownactive;
    float timer;
    float squadCooldown = 1.0f;

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
        SQUADMODE
    }


    private void Start()
    {
        navagent = gameObject.GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        Debug.Log(orderSelected);


        if (RadialMenu.squadselected == (int)RadialMenu.memberSelected.Light)
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
        //Debug.Log("Light Following");
        navagent.SetDestination(playerLocation.transform.position);
    }

    void Stop()
    {
        navagent.isStopped = true;
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
                Debug.Log("Light moving to cover");
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
