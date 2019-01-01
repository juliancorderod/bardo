using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuerteSubitaManager : MonoBehaviour
{
    public WorldManager wm;

    AudioSpectrum spectrum;

    public Material[] buildInsides;

    List<GameObject> realWindows = new List<GameObject>();

    public List<GameObject> animationList = new List<GameObject>();

    [HideInInspector]
    public List<GameObject> stars = new List<GameObject>();


    public float lightsLevelScale, starsLevelScale, smokeLevelScale, sphereRadius;
    float originalLightsScale, originalStarsScale, originalSmokeScale;

    public LayerMask windowsLayer;

    Light playerFloorLight;
    Color originalLightColor;

    NewPlayer player;
    float lightHue;

    float shortDelay;

    public GameObject kasWalkAnim, jBeer1, emiFum1, cafesinosa;

    float originalHue;

    bool haveReOrdered;

    RaycastHit hit;

    Vector3 rayPosAdjustment;

    bool animsActive;

    public smokeScript smoke;

    public Transform moon;
    Light luzLuna;
    Vector3 originalMoonEuler;
    float timer;

    bool willNeedReset = false;

    [HideInInspector]
    public Color streetLightColor;

    //[HideInInspector]
    //public List<GameObject> windows = new List<GameObject>();

    Collider[] windows;

    public CityBuilder c;

    // Use this for initialization
    void Start()
    {

        playerFloorLight = GameObject.Find("floorLight").GetComponent<Light>();
        originalLightColor = playerFloorLight.color;
        player = GameObject.Find("newPlayer").GetComponent<NewPlayer>();

        spectrum = GameObject.FindGameObjectWithTag("songMan").GetComponent<AudioSpectrum>();

        windows = Physics.OverlapSphere(new Vector3(25, 30, 9), sphereRadius, windowsLayer);

        //preLights = new MeshRenderer[windows.Length];



        for (int i = 0; i < buildInsides.Length; i++)
        {
            buildInsides[i].color = Color.HSVToRGB(0, 0, Random.Range(0.45f, 0.95f));
        }


        originalHue = lightHue;
        originalMoonEuler = moon.eulerAngles;

        originalLightsScale = lightsLevelScale;
        originalStarsScale = starsLevelScale;
        originalSmokeScale = smokeLevelScale;

        luzLuna = moon.GetComponent<Light>();

    }





    // Update is called once per frame
    void Update()
    {
        if (!haveReOrdered)
        {
            ReorderStuff();

            haveReOrdered = true;

        }

        //actual update start here!

        if (player.location == NewPlayer.Location.CITY)
        {
            willNeedReset = true;
            if (shortDelay < 0f)
            {
                //shortDelay += Time.deltaTime;//para saltarse ese segundito que se prenden las luces
                //for (int i = 0; i < preLights.Length; i++)
                //{
                //    if (preLights[i] != null)
                //        preLights[i].material.color = Color.Lerp(preLights[i].material.color, Color.clear, shortDelay / 2);


                //}
            }
            else//actually start messing with stuff here!
            {
                if (!animsActive)
                {
                    for (int i = 0; i < animationList.Count; i++)
                    {
                        animationList[i].SetActive(true);
                    }
                    animsActive = true;
                }

                //buuldings

                if (lightHue < 0)
                    lightHue = 1;
                else
                {
                    if (lightHue < 0.45f && lightHue > 0.12f)
                        lightHue -= Time.deltaTime / 30;
                    else
                        lightHue -= Time.deltaTime / 350;
                }

                //                Debug.Log(lightHue);

                for (int i = 0; i < buildInsides.Length; i++)
                {

                    buildInsides[i].color = Color.HSVToRGB(lightHue, 1, spectrum.MeanLevels[i] * lightsLevelScale);


                }
                float h, s, v;
                Color.RGBToHSV(buildInsides[3].color, out h, out s, out v);



                //stars:

                for (int i = 0; i < stars.Count; i++)
                {
                    Vector3 size = Vector3.one * 2 * (1 + (spectrum.MeanLevels[(i % 2) + 8] * starsLevelScale));
                    if (size.x < 4f)
                        stars[i].transform.localScale = size;
                    //the (i % 2) + 8 always returns 8,9 in this case
                    else
                        stars[i].transform.localScale = Vector3.one * 4f;

                }

                //smoke

                if (spectrum.MeanLevels[6] < 0.005f)
                    smoke.spread = 0.0002f;
                else
                    //smoke.spread = (Mathf.Pow(spectrum.MeanLevels[4], 2)) * smokeLevelScale;
                    smoke.spread = 0.03f;


                timer += Time.deltaTime;
                moon.eulerAngles = Vector3.Lerp(originalMoonEuler, new Vector3(40, 120, 284), timer / 200);

            }



            playerFloorLight.enabled = false;

            luzLuna.color = Color.HSVToRGB(lightHue, 0.7f, 0.7f);
        }
        else
        {
            if (willNeedReset)
            {
                ResetMuerteSubita();
                willNeedReset = false;
            }
        }


    }


    void ResetMuerteSubita()
    {

        for (int i = 0; i < buildInsides.Length; i++)
        {
            buildInsides[i].color = Color.HSVToRGB(0, 0, Random.Range(0.45f, 0.95f));

        }

        for (int i = 0; i < animationList.Count; i++)
        {
            animationList[i].SetActive(false);
        }
        animsActive = false;

        playerFloorLight.enabled = true;

        playerFloorLight.color = originalLightColor;
        lightHue = originalHue;

        shortDelay = 0;

    }

    public void adjustScales()
    {

        lightsLevelScale = originalLightsScale * wm.masterScaleSpectrum;
        starsLevelScale = originalStarsScale * wm.masterScaleSpectrum;
        smokeLevelScale = originalSmokeScale * wm.masterScaleSpectrum;

    }



    void ReorderStuff()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            if (windows[i] != null)
            {
                realWindows.Add(windows[i].gameObject);
            }
        }

        AssignMatPlaceAnims();

        //    for (int i = 0; i < preLights.Length; i++)
        //    {
        //        if (preLights[i] != null)
        //        {


        //            if (preLights[i].name == "pCube1" || preLights[i].name == "pCube2")
        //                rayPosAdjustment = Vector3.zero;
        //            else
        //                rayPosAdjustment = new Vector3(0, 0, -5);



        //            Ray checkIfInSight = new Ray(transform.position, (preLights[i].transform.position + rayPosAdjustment) - transform.position);
        //            Physics.Raycast(checkIfInSight, out hit, Vector3.Distance(preLights[i].transform.position, transform.position), windowsLayer);

        //            if (hit.transform == preLights[i].transform)
        //            {
        //                lights.Add(preLights[i]); //add it to the list here!
        //            }
        //        }
        //    }

        //    // Debug.Log(lights.Count);


        //    for (int i = 0; i < lights.Count; i++)
        //    {
        //        bool placedAnim = false;
        //        //lights[i].material.color = Color.red;

        //        if (!placedAnim)
        //        {
        //            if (lights[i].name == "pCube44" || lights[i].name == "pCube43")
        //            {
        //                if (Random.Range(0, 10) > 8)
        //                {
        //                    GameObject kwa = Instantiate(kasWalkAnim, lights[i].transform);

        //                    kwa.transform.localPosition = new Vector3(0, -0.5f, -4.4f);
        //                    animationList.Add(kwa);
        //                    placedAnim = true;
        //                }
        //            }
        //        }
        //        if (!placedAnim)
        //        {
        //            if (lights[i].name == "pCube45" || lights[i].name == "pCube43")
        //            {
        //                if (lights[i].transform.position.y < 40 && lights[i].transform.position.x < 100)
        //                {
        //                    if (Random.Range(0, 10) > 5)
        //                    {
        //                        GameObject jb1 = Instantiate(jBeer1, lights[i].transform);
        //                        if (Random.Range(0, 10) > 5)
        //                        {
        //                            jb1.transform.localPosition = new Vector3(-4.15f, 0f, -4.4f);
        //                        }
        //                        else
        //                        {
        //                            jb1.transform.localPosition = new Vector3(4.15f, 0f, -4.4f);
        //                            jb1.GetComponent<SpriteRenderer>().flipX = true;
        //                        }

        //                        jb1.GetComponent<Animator>().speed = Random.Range(0.8f, 1.2f);
        //                        animationList.Add(jb1);
        //                        placedAnim = true;
        //                    }
        //                }
        //            }
        //        }
        //        if (!placedAnim)
        //        {
        //            if (lights[i].name == "pCube44" || lights[i].name == "pCube43")
        //            {
        //                if (lights[i].transform.position.y > 20 && lights[i].transform.position.x < 100)
        //                {
        //                    if (Random.Range(0, 10) > 6)
        //                    {
        //                        GameObject ef1 = Instantiate(emiFum1, lights[i].transform);

        //                        ef1.transform.localPosition = new Vector3(1.6f, -0.5f, -4.4f);

        //                        ef1.GetComponent<Animator>().speed = Random.Range(0.5f, 1f);
        //                        animationList.Add(ef1);
        //                        placedAnim = true;
        //                    }
        //                }
        //            }
        //        }
        //        if (!placedAnim)
        //        {
        //            if (lights[i].name == "pCube44" || lights[i].name == "pCube43")
        //            {
        //                if (lights[i].transform.position.y > 10 && lights[i].transform.position.x < 100)
        //                {
        //                    if (Random.Range(0, 10) > 6)
        //                    {
        //                        GameObject caf = Instantiate(cafesinosa, lights[i].transform);

        //                        caf.transform.localPosition = new Vector3(0, -0.5f, -4.4f);

        //                        caf.GetComponent<Animator>().speed = Random.Range(0.7f, 1.2f);
        //                        animationList.Add(caf);
        //                        placedAnim = true;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    for (int i = 0; i < animationList.Count; i++)
        //    {
        //        animationList[i].SetActive(false);
        //    }
        //    animsActive = false;

        //    //            Debug.Log(animationList.Count);
    }
    void AssignMatPlaceAnims()
    {

        for (int i = 0; i < realWindows.Count; i++)
        {

            //preLights[i] = windows[i].GetComponent<MeshRenderer>();

            realWindows[i].GetComponent<MeshRenderer>().material = buildInsides[i % 10];


            //place anims!

            bool placedAnim = false;
            //lights[i].material.color = Color.red;

            if (!placedAnim)
            {
                if (realWindows[i].name == "pCube44" || realWindows[i].name == "pCube43")
                {
                    if (Random.Range(0, 10) > 8)
                    {
                        GameObject kwa = Instantiate(kasWalkAnim, realWindows[i].transform);

                        kwa.transform.localPosition = new Vector3(0, -0.5f, -4.4f);
                        animationList.Add(kwa);
                        placedAnim = true;
                    }
                }
            }
            if (!placedAnim)
            {
                if (realWindows[i].name == "pCube45" || realWindows[i].name == "pCube43")
                {
                    if (realWindows[i].transform.position.y < 40 && realWindows[i].transform.position.x < 100)
                    {
                        if (Random.Range(0, 10) > 5)
                        {
                            GameObject jb1 = Instantiate(jBeer1, realWindows[i].transform);
                            if (Random.Range(0, 10) > 5)
                            {
                                jb1.transform.localPosition = new Vector3(-4.15f, 0f, -4.4f);
                            }
                            else
                            {
                                jb1.transform.localPosition = new Vector3(4.15f, 0f, -4.4f);
                                jb1.GetComponent<SpriteRenderer>().flipX = true;
                            }

                            jb1.GetComponent<Animator>().speed = Random.Range(0.8f, 1.2f);
                            animationList.Add(jb1);
                            placedAnim = true;
                        }
                    }
                }
            }
            if (!placedAnim)
            {
                if (realWindows[i].name == "pCube44" || realWindows[i].name == "pCube43")
                {
                    if (realWindows[i].transform.position.y > 20 && realWindows[i].transform.position.x < 100)
                    {
                        if (Random.Range(0, 10) > 6)
                        {
                            GameObject ef1 = Instantiate(emiFum1, realWindows[i].transform);

                            ef1.transform.localPosition = new Vector3(1.6f, -0.5f, -4.4f);

                            ef1.GetComponent<Animator>().speed = Random.Range(0.5f, 1f);
                            animationList.Add(ef1);
                            placedAnim = true;
                        }
                    }
                }
            }
            if (!placedAnim)
            {
                if (realWindows[i].name == "pCube44" || realWindows[i].name == "pCube43")
                {
                    if (realWindows[i].transform.position.y > 10 && realWindows[i].transform.position.x < 100)
                    {
                        if (Random.Range(0, 10) > 6)
                        {
                            GameObject caf = Instantiate(cafesinosa, realWindows[i].transform);

                            caf.transform.localPosition = new Vector3(0, -0.5f, -4.4f);

                            caf.GetComponent<Animator>().speed = Random.Range(0.7f, 1.2f);
                            animationList.Add(caf);
                            placedAnim = true;
                        }
                    }
                }
            }
        }

        for (int i = 0; i < animationList.Count; i++)
        {
            animationList[i].SetActive(false);
        }
        animsActive = false;
        //        Debug.Log(animationList.Count);
    }
}
