using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    public GameObject[] crosshairs;
    public GameObject activeCrosshair;

    Quaternion crosshairRotation;

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
        if (SquadController.currentMode == (int)SquadController.AimingMode.Shoot)
        {
            setcrosshair(crosshairs[0]);
        }

        if (SquadController.currentMode == (int)SquadController.AimingMode.Squad)
        {
            setcrosshair(crosshairs[1]);
        }
    }
}
