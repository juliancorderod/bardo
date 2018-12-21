using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomHSV : MonoBehaviour
{

    MuerteSubitaManager m;

    // Use this for initialization
    void Start()
    {

        //setRandom();

        m = GameObject.Find("muerte subita").GetComponent<MuerteSubitaManager>();

        //        m.windows.Add(gameObject);

    }

    public void setRandom()
    {

        GetComponent<MeshRenderer>().material.color = Color.HSVToRGB(0, 0, Random.Range(0.00f, 1.00f));
        //this.enabled = false;
    }


}
