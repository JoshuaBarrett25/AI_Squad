using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverScript : MonoBehaviour
{
    public bool hitmaterial;

    private void Update()
    {
        if (hitmaterial)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }

        if (!hitmaterial)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
