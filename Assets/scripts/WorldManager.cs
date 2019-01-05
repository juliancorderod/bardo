using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    public Transform player;
    public List<GameObject> deactivateObj = new List<GameObject>();
    public float activationDist;
    public float xPos, zPos;

    //[HideInInspector]
    public float masterScaleSpectrum = 1;
    public FuturoManager futuroMan;
    public MuerteSubitaManager muerteMan;
    public FireFlyManager fireFlyMan;
    public SkyThingsManager skyThingsMan;

    public GameObject workDirLight;
    public Text wmScaleDebug;
    public GameObject canvas;

    // Use this for initialization
    void Start()
    {
        workDirLight.SetActive(false);
        wmScaleDebug.gameObject.SetActive(false);

#if UNITY_WEBGL
        masterScaleSpectrum = 0.4f;
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            //if (masterScaleSpectrum > 0.3f)
            //{
            masterScaleSpectrum /= 1.2f;
            futuroMan.adjustScales();
            muerteMan.adjustScales();
            fireFlyMan.adjustScales();
            skyThingsMan.adjustScales();
            //}
        }

        if (Input.GetKeyDown(KeyCode.Equals))
        {
            masterScaleSpectrum *= 1.2f;
            futuroMan.adjustScales();
            muerteMan.adjustScales();
            skyThingsMan.adjustScales();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            masterScaleSpectrum = 1;
            futuroMan.adjustScales();
            muerteMan.adjustScales();
            skyThingsMan.adjustScales();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (wmScaleDebug.gameObject.activeSelf)
                wmScaleDebug.gameObject.SetActive(false);
            else
                wmScaleDebug.gameObject.SetActive(true);
        }

        if (wmScaleDebug.gameObject.activeSelf)
            wmScaleDebug.text = "wmScale: " + masterScaleSpectrum;


        if (Input.GetKeyDown(KeyCode.K))
        {
            if (futuroMan.kaleidescopeOn)
                futuroMan.kaleidescopeOn = false;
            else
                futuroMan.kaleidescopeOn = true;
        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            if (canvas.activeSelf)
                canvas.SetActive(false);
            else
                canvas.SetActive(true);
        }


        if (player.position.x > xPos + 2 || player.position.x < xPos - 2 ||
           player.position.z > zPos + 2 || player.position.z < zPos - 2)
        {
            //            Debug.Log("kas sucks");
            for (int i = 0; i < deactivateObj.Count; i++)
            {
                if (deactivateObj[i] != null)
                {
                    if (Vector3.Distance(player.position, deactivateObj[i].transform.position) < activationDist)
                    {
                        deactivateObj[i].SetActive(true);
                        deactivateObj.Remove(deactivateObj[i]);
                        i--;
                    }
                }
                else
                {
                    deactivateObj.Remove(deactivateObj[i]);
                    i--;
                }
            }

            xPos = player.position.x;
            zPos = player.position.z;
        }



    }
}
