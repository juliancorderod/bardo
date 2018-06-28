using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonDance : MonoBehaviour
{

    public AudioSpectrum spectrum;

    public Transform[] joints;

    public float levelScale, initScale;

    public bool visualizer;
    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        //can only have as many joints as bands (10)!
        //if (visualizer)
        //{
        for (int i = 0; i < joints.Length; i++)
        {
            joints[i].localScale = Vector3.one * (levelScale * spectrum.MeanLevels[i] + initScale);
            //            Debug.Log(i + "|||" + spectrum.MeanLevels[i]);
        }
        //}




    }
}
