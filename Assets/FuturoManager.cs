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

    public float refreshTrailFreq;

    float[] spectrumValue, returnValue, scrollSpeed;


    // Use this for initialization
    void Start()
    {




        spectrum = GameObject.FindGameObjectWithTag("songMan").GetComponent<AudioSpectrum>();

        c = wallMat.color;

        this.enabled = false;

        wallMat.SetColor("_EmissionColor", new Vector4(c.r, c.g, c.b, 0) * 0);

        scrollSpeed = new float[fireFlies.Length];


        for (int i = 0; i < fireFlies.Length; i++)//hacer que cada frame solo haga uno en realidad
        {
            Keyframe[] kf = new Keyframe[10];
            for (int k = 0; k < kf.Length; k++)
            {
                kf[k] = new Keyframe(k / (kf.Length - 1f), 0);

                Debug.Log(kf[k].time);
            }

            AnimationCurve curve = new AnimationCurve(kf);

            fireFlies[i].widthCurve = curve;

            scrollSpeed[i] = i + 2;
            //fireFlies[i].widthMultiplier = (fireFlies.Length - i) * 2;
        }

        spectrumValue = new float[fireFlies.Length];
        returnValue = new float[fireFlies.Length];





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
                        curve.MoveKey(k, new Keyframe(curve.keys[k].time + Time.deltaTime / scrollSpeed[i],
                                                                        curve.keys[k].value));


                        if (curve.keys[k].time > 0.98f)
                        {
                            curve.RemoveKey(k);
                            Keyframe k0 = new Keyframe(0.01f, 0);
                            curve.AddKey(k0);
                            spectrumValue[i] = spectrum.MeanLevels[i % 10] * fireFlyScale;
                            returnValue[i] = curve.keys[curve.keys.Length - 2].value;

                        }



                    }

                    if (curve.keys[1].value < spectrumValue[i])
                    {
                        curve.MoveKey(1, new Keyframe(curve.keys[1].time,
                                                      curve.keys[1].value + (Time.deltaTime)));
                    }

                    if (curve.keys[curve.keys.Length - 2].value > 0 && curve.keys[curve.keys.Length - 2].time > 0.85f)
                    {

                        float speed = returnValue[i] * Time.deltaTime * 4;

                        curve.MoveKey(curve.keys.Length - 2, new Keyframe(curve.keys[curve.keys.Length - 2].time,
                                                                          curve.keys[curve.keys.Length - 2].value - speed));

                    }


                    curve.MoveKey(curve.keys.Length - 1, new Keyframe(1, 0));

                    //                    Debug.Log(curve.keys.Length);

                    fireFlies[i].widthCurve = curve;
                    fireFlies[i].widthMultiplier = Mathf.Lerp(fireFlies[i].widthMultiplier, spectrum.PeakLevels[i % 10] * fireFlyScale, 0.2f);



                }


                //}

                wallMat.SetColor("_EmissionColor", new Vector4(c.r, c.g, c.b, 0) * Mathf.Pow(spectrum.MeanLevels[1], 2) * wallColScale);

            }


        }



    }
}
