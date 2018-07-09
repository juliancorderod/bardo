using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuerteSubitaManager : MonoBehaviour
{

    AudioSpectrum spectrum;

    public MeshRenderer[] lights;

    public float levelScale, sphereRadius;

    public LayerMask windowsLayer;

    Light playerFloorLight;
    Color originalLightColor;

    NewPlayer player;
    float lightHue;

    // Use this for initialization
    void Start()
    {
        playerFloorLight = GameObject.Find("floorLight").GetComponent<Light>();
        originalLightColor = playerFloorLight.color;
        player = GameObject.Find("newPlayer").GetComponent<NewPlayer>();

        spectrum = GameObject.FindGameObjectWithTag("songMan").GetComponent<AudioSpectrum>();

        Collider[] windows = Physics.OverlapSphere(new Vector3(25, 30, 9), sphereRadius, windowsLayer);

        lights = new MeshRenderer[windows.Length];

        for (int i = 0; i < windows.Length; i++)
        {

            lights[i] = windows[i].GetComponent<MeshRenderer>();

            lights[i].material.color = Color.black;
        }

        float s, v;
        Color.RGBToHSV(originalLightColor, out lightHue, out s, out v);

    }

    // Update is called once per frame
    void Update()
    {
        if (player.inSong)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                if (lights[i] != null)
                    lights[i].material.color = Color.HSVToRGB(0, 0, spectrum.MeanLevels[i % 10] * levelScale);
                //lights[i].intensity = spectrum.MeanLevels[i % 10] * levelScale;

                //            Debug.Log(i + "|||" + spectrum.MeanLevels[i]);
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
        }


        //        Debug.Log(lightHue);
    }
}
