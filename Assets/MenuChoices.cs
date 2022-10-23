using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuChoices : MonoBehaviour
{
    public Color baseColour;
    public Color onColour;
    public Image background;
    
    void Start()
    {
        background.color = baseColour;
    }


    public void Hover()
    {
        background.color = onColour;
    }


    public void Normal()
    {
        background.color = baseColour;
    }
}
