using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuturoManager : MonoBehaviour
{
    public WorldManager wm;
    AudioSpectrum spectrum;

    public fireFlyScript[] fireFlies;
    public float fireFlyScale, wallColScale;

    public NewPlayer p;

    public Material wallMat;

    Color c;

    float shortDelay;

    // Use this for initialization
    void Start()
    {

        spectrum = GameObject.FindGameObjectWithTag("songMan").GetComponent<AudioSpectrum>();

        c = wallMat.color;

        this.enabled = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (p.inSong)
        {
            if (shortDelay < 1.1f)
            {
                shortDelay += Time.deltaTime;//para saltarse ese segundito que se prenden las luces

            }
            else
            {

                for (int i = 0; i < fireFlies.Length; i++)
                {
                    fireFlies[i].speed = spectrum.MeanLevels[i % 10] * fireFlyScale;
                }

                wallMat.SetColor("_EmissionColor", new Vector4(c.r, c.g, c.b, 0) * spectrum.MeanLevels[1] * wallColScale);

            }


        }



    }
}
