using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public Transform player;
    public List<GameObject> deactivateObj = new List<GameObject>();
    public float activationDist;
    public float xPos, zPos;


    public float masterScaleSpectrum;
    public FuturoManager futuroMan;
    public MuerteSubitaManager muerteMan;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            if (masterScaleSpectrum > 0.3f)
            {
                masterScaleSpectrum -= 0.2f;
                futuroMan.adjustScales();
                muerteMan.adjustScales();
            }
        }

        if (Input.GetKeyDown(KeyCode.Equals))
        {
            masterScaleSpectrum += 0.2f;
            futuroMan.adjustScales();
            muerteMan.adjustScales();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            masterScaleSpectrum = 1;
            futuroMan.adjustScales();
            muerteMan.adjustScales();
        }


        if (Input.GetKeyDown(KeyCode.K))
        {
            if (futuroMan.kaleidescopeOn)
                futuroMan.kaleidescopeOn = false;
            else
                futuroMan.kaleidescopeOn = true;
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
