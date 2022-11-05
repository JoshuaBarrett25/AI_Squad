using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    static public Transform pointLocation;
    public Camera fpsCamera;
    public LayerMask groundLayer;
    public LayerMask defendLayer;
    public CharacterController controller;
    static public int currentMode;

    bool cooldownactive;
    float timer;
    float squadCooldown = 1.0f;

    public float speed = 12f;

    public enum AimingMode
    {
        Shoot,
        Squad
    }



    private void Start()
    {
        fpsCamera = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Switch"))
        {
            //RadialMenu.isMemberSelected = true;
            currentMode = (int)AimingMode.Squad;
        }

        if (Input.GetButton("Cancel"))
        {
            RadialMenu.isMemberSelected = false;
            RadialMenu.isActionSelected = false;
            LightMember.pointToGo = false;
            HealerMember.pointToGo = false;
            ReconMember.pointToGo = false;
            HeavyMember.pointToGo = false;
            currentMode = (int)AimingMode.Shoot;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }
}
