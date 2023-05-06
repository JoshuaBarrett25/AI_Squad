using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GameObject player;
    static public Transform pointLocation;
    public Camera fpsCamera;
    public LayerMask groundLayer;
    public LayerMask defendLayer;
    public CharacterController controller;
    static public int currentMode;
    SquadBehaviour squadBehaviour;


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
        player = GameObject.FindGameObjectWithTag("Player");
        fpsCamera = Camera.main;
        squadBehaviour = player.GetComponent<SquadBehaviour>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Switch"))
        {
            currentMode = (int)AimingMode.Squad;
        }

        if (Input.GetButton("Cancel"))
        {
            RadialMenu.isMemberSelected = false;
            RadialMenu.isActionSelected = false;
            squadBehaviour.pointToGo = false;
            squadBehaviour.moveTo = false;
            currentMode = (int)AimingMode.Shoot;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }
}
