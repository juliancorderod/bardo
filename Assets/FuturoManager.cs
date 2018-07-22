using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuturoManager : MonoBehaviour
{
    public WorldManager wm;
    AudioSpectrum spectrum;

    public TrailRenderer[] fireFlies;

    public float fireFlyScale, wallColScale;

    public NewPlayer p;

    public Material wallMat;

    Color c;

    float shortDelay;

    float timer;
    public float refreshTrailFreq;

    // Use this for initialization
    void Start()
    {




        spectrum = GameObject.FindGameObjectWithTag("songMan").GetComponent<AudioSpectrum>();

        c = wallMat.color;

        this.enabled = false;

        wallMat.SetColor("_EmissionColor", new Vector4(c.r, c.g, c.b, 0) * 0);


        //for (int i = 0; i < fireFlies.Length; i++)//hacer que cada frame solo haga uno en realidad
        //{
        //    AnimationCurve curve = new AnimationCurve();

        //    curve.AddKey(0, 0);
        //    curve.AddKey(1 / 7, 0);
        //    curve.AddKey(2 / 7, 0);
        //    curve.AddKey(3 / 7, 0);
        //    curve.AddKey(4 / 7, 0);
        //    curve.AddKey(5 / 7, 0);
        //    curve.AddKey(6 / 7, 0);
        //    curve.AddKey(1, 0);


        //    fireFlies[i].widthCurve = curve;
        //}

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


                for (int i = 0; i < fireFlies.Length; i++)//hacer que cada frame solo haga uno en realidad
                {


                    AnimationCurve curve = fireFlies[i].widthCurve;

                    for (int k = 1; k < curve.keys.Length - 1; k++)
                    {
                        //Debug.Log(k);
                        //fireFlies[i].widthCurve.keys[k] = new Keyframe(fireFlies[i].widthCurve.keys[k].time + Time.deltaTime / 2,
                        //fireFlies[i].widthCurve.keys[k].value);

                        curve.MoveKey(k, new Keyframe(curve.keys[k].time + Time.deltaTime / 6,
                                                                        curve.keys[k].value));



                        if (curve.keys[k].time > 0.9)
                        {
                            curve.RemoveKey(k);
                            Keyframe k0 = new Keyframe(0.05f, spectrum.MeanLevels[i % 10] * fireFlyScale);
                            curve.AddKey(k0);
                        }



                    }

                    curve.MoveKey(curve.keys.Length - 1, new Keyframe(1, 0));

                    Debug.Log(curve.keys.Length);

                    fireFlies[i].widthCurve = curve;

                }

                timer = 0;
                //}

                wallMat.SetColor("_EmissionColor", new Vector4(c.r, c.g, c.b, 0) * Mathf.Pow(spectrum.MeanLevels[1], 2) * wallColScale);

            }


        }



    }
}
