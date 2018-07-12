using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomHSV : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        GetComponent<MeshRenderer>().material.color = Color.HSVToRGB(0, 0, Random.Range(0.00f, 1.00f));
        this.enabled = false;

    }


}
