﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kino;

public class SkyThingsManager : MonoBehaviour
{

    public WorldManager wm;
    AudioSpectrum spectrum;
    public NewPlayer p;


    public List<Transform> skyThings = new List<Transform>();
    public GameObject gridObj;
    public GameObject trailPrefab;
    public Transform freeTrailsParent;

    public Texture2D textToSampleFrom;

    public float sphereSize = 500;
    float xMin = -400, xMax = 400, yMin = -400, yMax = 400;
    public float thingySpeed = 100, turnSpeed = 5;
    public int amountOfThingys = 25, totalTrails = 400;


    [Header("if no texture:")]
    public float noiseSize = 4f;

    List<TrailRenderer> freeTrails = new List<TrailRenderer>();
    List<TrailRenderer> allTrails = new List<TrailRenderer>();

    [Header("spectrum stuff:")]
    public float spectrumSpeedScale;
    public float pulseScale;
    float noiseSampleScale = 1;
    float originalPulseScale, originalSpectrumSpeedScale, originalNoiseSampleScale;

    float fogColH, fogColS, fogColV;

    // Use this for initialization
    void Start()
    {

        Color.RGBToHSV(RenderSettings.fogColor, out fogColH, out fogColS, out fogColV);

        Debug.Log(fogColS);

        for (int i = 0; i < totalTrails; i++)
        {
            GameObject t = Instantiate(trailPrefab, freeTrailsParent);

            freeTrails.Add(t.GetComponent<TrailRenderer>());
            allTrails.Add(t.GetComponent<TrailRenderer>());
            //t.GetComponent<TrailRenderer>().endColor = new Color(i / 10.0f, i / 10.0f, i / 10.0f);
            t.SetActive(false);
        }


        for (int i = 0; i < amountOfThingys; i++)
        {
            Vector3 randOnSphere = Random.onUnitSphere * sphereSize;
            if (randOnSphere.y > transform.position.y)
            {
                GameObject g = new GameObject();
                g.transform.parent = transform;
                g.transform.localPosition = randOnSphere;// new Vector3(Random.Range(xMin * 0.8f, xMax * 0.8f), Random.Range(yMin * 0.8f, yMax * 0.8f), 495);
                g.name = "skyThing " + i;
                skyThings.Add(g.transform);
                AssignTrail(g.transform, i);

                g.SetActive(false);
            }
            else
                i--;
        }


        //for (int x = 0; x < 20000; x++)
        //{

        //    Vector3 randOnSphere = Random.onUnitSphere * sphereSize;
        //    if (randOnSphere.y > transform.position.y)
        //    {
        //        GameObject g = Instantiate(gridObj, randOnSphere, Quaternion.Euler(0, 0, 0));
        //        g.transform.localScale = new Vector3(10, 10, 1);
        //        g.transform.LookAt(transform.position);

        //        float n = GetPNoise(g.transform.position.x, g.transform.position.y, g.transform.position.z);
        //        g.GetComponent<SpriteRenderer>().color = new Color(n, n, n);
        //    }
        //    else
        //        x--;

        //}


        spectrum = GameObject.FindGameObjectWithTag("songMan").GetComponent<AudioSpectrum>();

        originalPulseScale = pulseScale;
        originalSpectrumSpeedScale = spectrumSpeedScale;
        originalNoiseSampleScale = noiseSampleScale;


    }

    float sampleTimer;
    float totalSampleVal;
    float activateSkyThingTimer, waitB4ActivatingTimer, time2Waitb4Activating = 10;
    int activateIndex;

    bool willNeedReset;
    // Update is called once per frame
    void Update()
    {
        if (p.location == NewPlayer.Location.MOUNTAINS)
        {
            willNeedReset = true;

            //------activate parents--------

            if (waitB4ActivatingTimer < time2Waitb4Activating)
            {
                waitB4ActivatingTimer += Time.deltaTime;
            }
            else
            {
                if (activateIndex < skyThings.Count)
                {
                    if (activateSkyThingTimer < 10)
                        activateSkyThingTimer += Time.deltaTime * ((activateIndex * 0.5f) + 1);
                    else
                    {
                        //                        Debug.Log("activated " + activateIndex);
                        skyThings[activateIndex].gameObject.SetActive(true);
                        activateSkyThingTimer = 0;
                        activateIndex++;
                    }
                }
            }

            //------pulse------
            for (int i = 0; i < allTrails.Count; i++)
            {
                if (allTrails[i].gameObject.activeSelf)
                    allTrails[i].widthMultiplier = Geo.remapRange(allTrails[i].sortingOrder, 0, 9, 1.5f, 3f) + (spectrum.MeanLevels[1] * pulseScale);
            }




            //-------cambio de noise sampleando por cierto tiempo---------

            if (sampleTimer < 10)//o en cambio de cancion
            {
                sampleTimer += Time.deltaTime;
                for (int i = 0; i < spectrum.Levels.Length; i++)
                {
                    totalSampleVal += spectrum.Levels[i];
                }

            }
            else
            {
                float avgSampleVal = totalSampleVal / sampleTimer;
                avgSampleVal *= noiseSampleScale;
                //            Debug.Log(avgSampleVal);


                avgSampleVal = Mathf.Clamp(avgSampleVal, 7, 27);
                noiseSize = Geo.remapRange(avgSampleVal, 7, 27, 0, 12);
                sampleTimer = 0;
                totalSampleVal = 0;
            }

            //------movimiento------
            for (int i = 0; i < skyThings.Count; i++)
            {
                if (skyThings[i].gameObject.activeSelf)
                {

                    float textVal = GetPTexture(skyThings[i].localPosition.x,
                                                skyThings[i].localPosition.y,
                                                skyThings[i].localPosition.z);
                    float textValNext = GetPTexture(skyThings[i].localPosition.x + skyThings[i].right.x * 3,
                                                    skyThings[i].localPosition.y + skyThings[i].right.y * 3,
                                                    skyThings[i].localPosition.z + skyThings[i].right.z * 3);




                    float turnVal = (textVal - textValNext);

                    //if(skyThings[i].position.y > )

                    //float turnVal = (i + 0.5f / 10) - textValNext;

                    float spectrumSpeed = (spectrum.MeanLevels[i % 10] * spectrumSpeedScale) + 0.2f;
                    spectrumSpeed = spectrumSpeed * thingySpeed;

                    skyThings[i].eulerAngles += new Vector3(0, 0, turnVal * Time.deltaTime * spectrumSpeed * turnSpeed);
                    skyThings[i].localPosition += skyThings[i].right * Time.deltaTime * spectrumSpeed;
                    //skyThings[i].position -= skyThings[i].forward * Time.deltaTime * 1f;

                    bool wrappedAround = false;
                    //if (Mathf.Abs(skyThings[i].position.x) > xMax)
                    //{
                    //    wrappedAround = true;
                    //}
                    //if (Mathf.Abs(skyThings[i].position.y) > yMax)
                    //{
                    //    wrappedAround = true;
                    //}
                    if (skyThings[i].localPosition.y < -150)
                    {
                        wrappedAround = true;
                    }

                    if (wrappedAround)
                    {
                        RemoveTrail(skyThings[i]);

                        Vector3 randOnSphere = Random.onUnitSphere * sphereSize;
                        while (randOnSphere.y < transform.position.y)
                        {
                            randOnSphere = Random.onUnitSphere * sphereSize;
                        }
                        skyThings[i].position = randOnSphere; //new Vector3(Random.Range(xMin * 0.8f, xMax * 0.8f), Random.Range(yMin * 0.8f, yMax * 0.8f), 495);
                        AssignTrail(skyThings[i], i);
                    }

                    float zRot = skyThings[i].localEulerAngles.z;
                    skyThings[i].LookAt(transform.position);
                    skyThings[i].eulerAngles = new Vector3(skyThings[i].eulerAngles.x, skyThings[i].eulerAngles.y, zRot);

                }
            }


        }
        else
        {
            if (willNeedReset)
            {
                ResetSkyThings();
                willNeedReset = false;
            }
        }


    }

    void RemoveTrail(Transform parent)
    {
        Transform trail = parent.GetChild(0).transform;
        freeTrails.Add(trail.GetComponent<TrailRenderer>());
        trail.parent = freeTrailsParent;

    }

    void AssignTrail(Transform parent, int index)
    {
        if (freeTrails.Count > 0)
        {
            freeTrails[0].enabled = false;
            freeTrails[0].transform.parent = parent;
            freeTrails[0].gameObject.SetActive(true);
            freeTrails[0].transform.localPosition = Vector3.zero;
            freeTrails[0].startColor = Color.HSVToRGB(fogColH, fogColS, Geo.remapRange(index % 10, 0, 9, 0.8f, 2f));
            freeTrails[0].endColor = RenderSettings.fogColor;
            freeTrails[0].sortingOrder = index % 10;
            freeTrails[0].Clear();
            freeTrails[0].enabled = true;
            freeTrails.Remove(freeTrails[0]);

        }
    }



    float GetPTexture(float x, float y, float z)
    {
        if (textToSampleFrom != null)
        {
            Color pixelCol = textToSampleFrom.GetPixel(Mathf.RoundToInt(Geo.remapRange(x, xMin, xMax, 0, textToSampleFrom.width)),
                                                    Mathf.RoundToInt(Geo.remapRange(y, yMin, yMax, 0, textToSampleFrom.height)));
            float value = pixelCol.grayscale;

            return value;
        }
        else
            return GetPNoise(x, y, z);
    }

    float GetPNoise(float x, float y, float z)
    {

        //float noise = Mathf.PerlinNoise(Geo.remapRange(x, -sphereSize, sphereSize, 0, 1) * noiseSize,
        //Geo.remapRange(y, -sphereSize, sphereSize, 0, 1) * noiseSize);

        float noise = Perlin.Noise(Geo.remapRange(x, -sphereSize, sphereSize, 0, 1) * noiseSize,
        Geo.remapRange(y, -sphereSize, sphereSize, 0, 1) * noiseSize,
        Geo.remapRange(z, -sphereSize, sphereSize, 0, 1) * noiseSize);


        return noise;

    }

    void ResetSkyThings()
    {
        //hacerles que vuelvan a aparecer poco a poco
        for (int i = 0; i < skyThings.Count; i++)
        {
            skyThings[i].gameObject.SetActive(false);
        }

        waitB4ActivatingTimer = 0;
        time2Waitb4Activating = 5;
        activateSkyThingTimer = 0;
        activateIndex = 0;
    }

    public void adjustScales()
    {
        noiseSampleScale = originalNoiseSampleScale * wm.masterScaleSpectrum;
        spectrumSpeedScale = originalSpectrumSpeedScale * wm.masterScaleSpectrum;
        pulseScale = originalPulseScale * wm.masterScaleSpectrum;
    }
}


