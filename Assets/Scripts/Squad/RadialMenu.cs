using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{
    public GameObject[] menuObjects;

    public Vector2 mouseVector;
    public float angle;
    public int selected;

    static public bool isMemberSelected;
    static public bool isActionSelected;

    private void Start()
    {
        for (int i = 0; i < menuObjects.Length; i++)
        {
            MenuToggle(menuObjects, i, false);
        }
    }

    void Update()
    {
        if (SquadController.currentMode == (int)SquadController.AimingMode.Squad)
        {
            Radian();
            if (!isMemberSelected)
            {
                MenuToggle(menuObjects, 0, true);
                MemberSelector();
            }

            if (isMemberSelected && !isActionSelected)
            {
                MenuToggle(menuObjects, 1, true);
                ActionSelector();
            }
        }
    }


    void MenuToggle(GameObject[] menu, int index, bool state)
    {
        //Want menu to be enabled
        if (state)
        {
            menu[index].SetActive(true);
        }

        //Want menu to be disabled
        if (!state)
        {
            menu[index].SetActive(false);
        }    
    }


    void Radian()
    {
        mouseVector = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);
        angle = Mathf.Atan2(mouseVector.y, mouseVector.x)*Mathf.Rad2Deg;
        angle = (angle + 360) % 360;

        selected = (int)(angle / 90);
    }


    void MouseCursor(bool displayCursor)
    {
        if (displayCursor)
        {
            Cursor.visible = true;
            Time.timeScale = 0;
        }

        if (!displayCursor)
        {
            Cursor.visible = false;
            Time.timeScale = 1;
        }
    }


    void MemberSelector()
    {
        MouseCursor(true);
        if (Input.GetMouseButtonUp(1) && SquadController.currentMode == (int)SquadController.AimingMode.Squad)
        {
            Debug.Log("MemberSelected: " + selected);
            SquadController.squadmemberselected = selected;
            isMemberSelected = true;
            MenuToggle(menuObjects, 0, false);
        }
    }

    void ActionSelector()
    {
        MouseCursor(true);
        if (Input.GetMouseButtonDown(1) && SquadController.currentMode == (int)SquadController.AimingMode.Squad)
        {
            Debug.Log("ActionSelected: " + selected);
            SquadController.squadorderSelected = selected;
            isActionSelected = true;
            MenuToggle(menuObjects, 1, false);
            MouseCursor(false);
        }
    }
}
