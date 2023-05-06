using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class HealerMember : MonoBehaviour
{
    SquadBehaviour squadBehaviour;
    GameObject player;
    GameObject cover;
    NavMeshAgent navagent;
    [SerializeField] Transform formationLocation;
    static public int orderSelected;

    public Camera camera;

    GameObject enemyToAttack;
    bool enemyFound;


    public enum AimingMode
    {
        Shoot,
        Squad
    }

    public enum memberOrders
    {
        Stop,
        Follow,
        Attack,
        Defend,
        SQUADMODE
    }


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cover = GameObject.FindGameObjectWithTag("Cover");
        squadBehaviour = player.GetComponent<SquadBehaviour>();
        navagent = gameObject.GetComponent<NavMeshAgent>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyToAttack = other.gameObject;
            enemyFound = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        enemyFound = false;
    }

    void Update()
    {
        if (RadialMenu.squadselected == (int)RadialMenu.memberSelected.Healer)
        {
            orderSelected = RadialMenu.orderselected;
        }

        switch (orderSelected)
        {
            case (int)memberOrders.Stop:
                squadBehaviour.Stop(navagent);
                break;

            case (int)memberOrders.Follow:
                squadBehaviour.Follow(navagent, formationLocation);
                break;

            case (int)memberOrders.Attack:
                if (!enemyFound)
                {
                    if (!squadBehaviour.moveTo)
                    {

                        squadBehaviour.Move(navagent, camera);
                    }
                    if (squadBehaviour.moveTo)
                    {
                        if (Input.GetButtonDown("Cancel"))
                        {
                            orderSelected = (int)memberOrders.Stop;
                        }
                        squadBehaviour.AimCast(navagent, camera);
                    }
                }
                break;

            case (int)memberOrders.Defend:
                if (!squadBehaviour.pointToGo)
                {
                    squadBehaviour.Point();
                }

                if (squadBehaviour.pointToGo)
                {
                    squadBehaviour.DefendCast(navagent, camera, cover.layer);
                }
                break;
        }
    }
}
