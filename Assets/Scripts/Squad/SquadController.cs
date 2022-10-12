using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadController : MonoBehaviour
{
    public LayerMask layermask;
    Camera fpsCamera;
    public static int currentMode;
    float squadCooldown = 1.0f;
    float timer;

    public enum AimingMode
    {
        Shoot,
        Squad
    }

    void Start()
    {
        fpsCamera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if(Input.GetButton("Switch"))
        {
            currentMode = (int)AimingMode.Squad;
        }

        if(Input.GetButton("Cancel"))
        {
            currentMode = (int)AimingMode.Shoot;
        }

        Debug.Log(currentMode);

        if (currentMode == (int)AimingMode.Squad)
        {
            SquadSelectionMode();
        }
    }

    void cooldown()
    {
        timer += Time.deltaTime;

        if (timer >= squadCooldown)
        {

        }
    }

    void SquadSelectionMode()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.TransformDirection(Vector3.forward), out hit, 30, layermask.value))
        {
            if (Input.GetButton("Fire") && )
            {
                
            }
        }
    }
}
