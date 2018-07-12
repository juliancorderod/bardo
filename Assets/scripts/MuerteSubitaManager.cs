using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuerteSubitaManager : MonoBehaviour
{

    AudioSpectrum spectrum;

    MeshRenderer[] preLights;
    List<MeshRenderer> lights = new List<MeshRenderer>();

    public float levelScale, sphereRadius;

    public LayerMask windowsLayer;

    Light playerFloorLight;
    Color originalLightColor;

    NewPlayer player;
    float lightHue;

    float shortDelay;

    public GameObject kasWalkAnim;

    float originalHue;

    bool haveReOrdered;

    RaycastHit hit;

    // Use this for initialization
    void Start()
    {

        playerFloorLight = GameObject.Find("floorLight").GetComponent<Light>();
        originalLightColor = playerFloorLight.color;
        player = GameObject.Find("newPlayer").GetComponent<NewPlayer>();

        spectrum = GameObject.FindGameObjectWithTag("songMan").GetComponent<AudioSpectrum>();

        Collider[] windows = Physics.OverlapSphere(new Vector3(25, 30, 9), sphereRadius, windowsLayer);

        preLights = new MeshRenderer[windows.Length];
        Debug.Log(windows.Length);

        for (int i = 0; i < windows.Length; i++)
        {
            preLights[i] = windows[i].GetComponent<MeshRenderer>();
        }

        float s, v;
        Color.RGBToHSV(originalLightColor, out lightHue, out s, out v);

        originalHue = lightHue;
    }

    // Update is called once per frame
    void Update()
    {
        if (!haveReOrdered)
        {

            for (int i = 0; i < preLights.Length; i++)
            {
                if (preLights[i] != null)
                {

                    Ray checkIfInSight = new Ray(transform.position, (preLights[i].transform.position + new Vector3(0, 0, -5)) - transform.position);
                    Physics.Raycast(checkIfInSight, out hit, Vector3.Distance(preLights[i].transform.position, transform.position), windowsLayer);

                    if (hit.transform == preLights[i].transform)
                    {
                        lights.Add(preLights[i]); //add it to the list here!
                    }
                }
            }

            Debug.Log(lights.Count);


            for (int i = 0; i < lights.Count; i++)
            {

                //lights[i].material.color = Color.red;


                if (lights[i].name == "pCube44" || lights[i].name == "pCube43")
                {
                    if (Random.Range(0, 10) > 8)
                    {
                        GameObject kwa = Instantiate(kasWalkAnim, lights[i].transform);

                        kwa.transform.localPosition = new Vector3(0, 0f, -4.35f);
                        Debug.Log("kwa");
                    }
                }


            }

            haveReOrdered = true;
            this.enabled = false;
        }


        if (player.inSong)
        {
            if (shortDelay < 1.1f)
            {
                shortDelay += Time.deltaTime;//para saltarse ese segundito que se prenden las luces
                for (int i = 0; i < preLights.Length; i++)
                {
                    if (preLights[i] != null)
                        preLights[i].material.color = Color.Lerp(preLights[i].material.color, Color.clear, shortDelay / 2);


                }
            }
            else
            {
                for (int i = 0; i < lights.Count; i++)
                {

                    //lights[i].material.color = Color.HSVToRGB(0, 0, spectrum.MeanLevels[i % 10] * levelScale);
                    lights[i].material.color = new Color(1, 1, 1, spectrum.MeanLevels[i % 10] * levelScale);
                    //lights[i].intensity = spectrum.MeanLevels[i % 10] * levelScale;

                    //            Debug.Log(i + "|||" + spectrum.MeanLevels[i]);
                }
            }

            if (lightHue < 0)
                lightHue = 1;
            else
            {
                if (lightHue < 0.45f && lightHue > 0.12f)
                    lightHue -= Time.deltaTime / 30;
                else
                    lightHue -= Time.deltaTime / 350;
            }


            playerFloorLight.color = Color.HSVToRGB(lightHue, 1, 1);
        }
        else
        {
            playerFloorLight.color = originalLightColor;
            lightHue = originalHue;
        }


        //        Debug.Log(lightHue);
    }
}
