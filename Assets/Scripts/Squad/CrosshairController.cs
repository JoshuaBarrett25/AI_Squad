using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    public GameObject[] crosshairs;
    public GameObject activeCrosshair;

    private void Start()
    {
        crosshairs[0].SetActive(true);
        activeCrosshair = crosshairs[0];
        for (int i = 1; i < crosshairs.Length; i++)
        {
            crosshairs[i].SetActive(false);
        }
    }


    void setcrosshair(GameObject switchCrosshair)
    {
        activeCrosshair.SetActive(false);
        switchCrosshair.SetActive(true);
        activeCrosshair = switchCrosshair;
    }


    private void Update()
    {
        if (PlayerMovement.currentMode == (int)LightMember.AimingMode.Shoot)
        {
            setcrosshair(crosshairs[0]);
        }

        if (PlayerMovement.currentMode == (int)LightMember.AimingMode.Squad)
        {
            setcrosshair(crosshairs[1]);
        }
    }
}
