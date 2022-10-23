using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SquadController : MonoBehaviour
{
    int[] currentModeList = { 0, 0, 0, 0 };
    public GameObject[] members;
    public Transform[] formationlocations;
    public LayerMask layermask;
    Camera fpsCamera;
    public static int currentMode;
    float squadCooldown = 1.0f;
    float timer;
    bool cooldownactive = false;

    public Transform targetTransform;

    static public int squadmemberselected;
    static public int squadorderSelected;
    static public bool ordered;


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
        Disperse,
        SQUADMODE
    }

    enum memberSelected
    {
        Light,
        Heavy,
        Recon,
        Healer
    }


    void Start()
    {
        //squadorderSelected = (int)memberOrders.Follow;
        fpsCamera = GetComponentInChildren<Camera>();
    }


    void Update()
    {
        if(Input.GetButton("Switch"))
        {
            //RadialMenu.isMemberSelected = true;
            currentMode = (int)AimingMode.Squad;
        }

        if(Input.GetButton("Cancel"))
        {
            RadialMenu.isMemberSelected = false;
            RadialMenu.isActionSelected = false;
            currentMode = (int)AimingMode.Shoot;
        }

        cooldown();


        switch (squadorderSelected)
        { 
            case (int)memberOrders.Follow:
                FollowPlayer(members[squadmemberselected], formationlocations[squadmemberselected], false);
                break;

            case (int)memberOrders.Move:
                Move(members[squadmemberselected], false);
                break;

            case (int)memberOrders.Stop:
                Stop(members[squadmemberselected], false);
                break;

            case (int)memberOrders.Disperse:
                break;
        }
    }


    void cooldown()
    {
        if (cooldownactive)
        {
            timer += Time.deltaTime;

            if (timer >= squadCooldown)
            {
                cooldownactive = false;
                timer = 0;
            }
        }
    }


    void Move(GameObject member, bool isSquadAction)
    {

        RaycastHit hit;
        
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.TransformDirection(Vector3.forward), out hit, 30, layermask.value))
        {
            if (Input.GetButton("Fire") && !cooldownactive && isSquadAction)
            {
                GameObject[] squad = GameObject.FindGameObjectsWithTag("Squad");
                for (int i = 0; i < squad.Length; i++)
                {
                    NavMeshAgent navagent = squad[i].GetComponent<NavMeshAgent>();
                    //MemberBehaviour behaviour = squad[i].GetComponent<MemberBehaviour>();
                    navagent.SetDestination(hit.transform.position);
                }
                cooldownactive = true;
            }

            else if (Input.GetButton("Fire") && !cooldownactive && !isSquadAction)
            {
                NavMeshAgent navagent = member.GetComponent<NavMeshAgent>();
                navagent.SetDestination(hit.point);
                currentModeList[squadmemberselected] = (int)squadorderSelected;
                cooldownactive = true;
            }
        }
    }


    void FollowPlayer(GameObject member, Transform destination, bool isSquadAction)
    {
        if (isSquadAction)
        {
            for (int i = 0; i < members.Length; i++)
            {
                NavMeshAgent nav = members[i].GetComponent<NavMeshAgent>();
                nav.SetDestination(formationlocations[i].position);
            }
        }
        
        else if (!isSquadAction)
        {
            NavMeshAgent navagent = member.GetComponent<NavMeshAgent>();
            navagent.isStopped = false;
            currentModeList[squadmemberselected] = (int)squadorderSelected;
            Debug.Log("Follow");
            navagent.SetDestination(destination.position);
        }
    }


    void Attack(GameObject member, Transform destination, bool isSquadAction)
    {

    }

    void Stop(GameObject member, bool isSquadAction)
    {
        currentModeList[squadmemberselected] = (int)squadorderSelected;
        NavMeshAgent navagent = member.GetComponent<NavMeshAgent>();
        navagent.isStopped = true;
    }
}
