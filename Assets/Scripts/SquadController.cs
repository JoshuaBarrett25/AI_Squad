using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadController : MonoBehaviour
{
    public LayerMask layermask;
    Camera fpsCamera;
    int currentMode;

    enum AimingMode
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

        Debug.Log(currentMode);

        if (currentMode == (int)AimingMode.Squad)
        {
            SquadSelectionMode();
        }
    }


    void SquadSelectionMode()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.TransformDirection(Vector3.forward), out hit, 30, layermask.value))
        {
            Debug.Log("Hitting layer");
        }
    }
}
