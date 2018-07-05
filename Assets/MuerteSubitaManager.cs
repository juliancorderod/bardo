using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuerteSubitaManager : MonoBehaviour
{

    AudioSpectrum spectrum;

    public MeshRenderer[] objects;

    public float levelScale, sphereRadius;

    public LayerMask windowsLayer;

    NewPlayer player;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("newPlayer").GetComponent<NewPlayer>();

        spectrum = GameObject.FindGameObjectWithTag("songMan").GetComponent<AudioSpectrum>();

        Collider[] windows = Physics.OverlapSphere(transform.position, sphereRadius, windowsLayer);

        objects = new MeshRenderer[windows.Length];

        for (int i = 0; i < windows.Length; i++)
        {

            objects[i] = windows[i].GetComponent<MeshRenderer>();

            windows[i].GetComponent<MeshRenderer>().material.color = Color.black;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (player.inSong)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] != null)
                    objects[i].material.color = Color.HSVToRGB(0, 0, spectrum.MeanLevels[i % 10] * levelScale);

                //            Debug.Log(i + "|||" + spectrum.MeanLevels[i]);
            }
        }



    }
}
