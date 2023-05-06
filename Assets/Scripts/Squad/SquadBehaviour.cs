using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SquadBehaviour : MonoBehaviour
{
    public GameObject fpsCamera;
    public GameObject player;
    public LayerMask groundLayer;

    public Transform defendpointLocation;
    public Transform groundpointLocation;

    public bool pointToGo;
    public bool moveTo;
    bool previousFull;
    GameObject hitCover;
    GameObject previousHitCover;
    CoverScript coverScript;

    bool cooldownactive;
    float timer;
    float squadCooldown = 0.5f;

    bool initialAttack;
    [SerializeField] float attackTimer = 1.0f;
    [SerializeField] int attackDamage = 20;
    float initAttackTimer;

    static public int currentMode;

    public enum AimingMode
    {
        Shoot,
        Squad
    }



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fpsCamera = GameObject.FindGameObjectWithTag("MainCamera");
        initAttackTimer = (float)(attackTimer * 0.2);
    }

    void Update()
    {
        Cooldown();

        if (hitCover != null)
        {
            Debug.Log("Current cover hit: " + hitCover.name);
        }

        if (previousHitCover != null)
        {
            Debug.Log("Previous cover hit: " + previousHitCover.name);
        }
    }


    void Cooldown()
    {
        if (cooldownactive)
        {
            timer += Time.deltaTime;
            
            if (timer == squadCooldown)
            {
                timer = 0;
                cooldownactive = false;
            }
        }
    }


    

    public void Follow(NavMeshAgent navagent, Transform formation)
    {
        navagent.isStopped = false;
        navagent.SetDestination(formation.position);
    }

    public void Stop(NavMeshAgent navagent)
    {
        navagent.isStopped = true;
    }

    public void Move(NavMeshAgent navagent, Camera camera)
    {
        if (Input.GetButton("Fire") && !moveTo)
        {
            moveTo = true;
        }
    }

    public void AimCast(NavMeshAgent navagent, Camera camera)
    {
        RaycastHit hit;

        if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, 30, groundLayer.value))
        {
            if (Input.GetButton("Fire"))
            {
                navagent.SetDestination(hit.point);
            }
        }
    }


    public void Point()
    {
        if (Input.GetButton("Fire") && !pointToGo)
        {
            pointToGo = true;
            Debug.Log("Point");
        }
    }

    public void DefendCast(NavMeshAgent navagent, Camera fpsCamera, int defendLayerValue)
    {
        navagent.isStopped = false;
        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.TransformDirection(Vector3.forward), out hit, 30))
        {
            hitCover = hit.collider.gameObject;
            if (hitCover != previousHitCover && previousFull)
            {
                previousHitCover.GetComponent<CoverScript>().hitmaterial = false;
            }

            coverScript = hitCover.GetComponent<CoverScript>();
            if (coverScript != null)
            {
                coverScript.hitmaterial = true;

                if (Input.GetButton("Fire"))
                {
                    defendpointLocation = hit.transform;
                    navagent.SetDestination(defendpointLocation.position);
                    Debug.Log("moving");
                    cooldownactive = true;
                    coverScript.hitmaterial = false;
                }

                previousHitCover = hitCover;
                previousFull = true;
            }
        }

        else
        {
            if (previousHitCover != null)
            {
                previousHitCover.GetComponent<CoverScript>().hitmaterial = false;
                coverScript.hitmaterial = false;
            }
        }
    }


    public void Attacking(NavMeshAgent navagent, GameObject enemy, Transform destinationUponKill)
    {
        navagent.SetDestination(enemy.transform.position);
        if (navagent.remainingDistance < 1.0f)
        {
            navagent.isStopped = true;
            Debug.Log("Attacking");

            if (initialAttack)
            {
                initAttackTimer += Time.deltaTime;
                if (initAttackTimer >= attackTimer)
                {
                    enemy.GetComponent<Health>().TakeDamage(attackDamage);
                    initialAttack = false;
                    timer = 0;
                }
            }

            timer += Time.deltaTime;
            if (timer >= attackTimer)
            {
                enemy.GetComponent<Health>().TakeDamage(attackDamage);
                timer = 0;
            }

        }   
        
        if (navagent.remainingDistance >= 1.0f)
        {
            navagent.isStopped = false;
        }

        if (enemy == null)
        {
            navagent.isStopped = false;
            navagent.SetDestination(destinationUponKill.position);
        }
    }
}
