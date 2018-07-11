using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public Transform player;
    public List<GameObject> deactivateObj = new List<GameObject>();
    public float activationDist;
    public float xPos, zPos;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
