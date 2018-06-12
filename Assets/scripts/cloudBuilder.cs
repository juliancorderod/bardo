using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudBuilder : MonoBehaviour
{

    public GameObject cloud;
    public float maxClouds, distanceFromCenter;

    // Use this for initialization
    void Start()
    {

        for (int i = 0; i < maxClouds; i++)
        {

            GameObject g = Instantiate(cloud, new Vector3(Random.Range(-distanceFromCenter, distanceFromCenter),
                                                          Random.Range(30, 80),
                                                          Random.Range(-distanceFromCenter, distanceFromCenter))
                                       + transform.position, Quaternion.identity);

            g.transform.rotation = Random.rotation;
            g.transform.localScale = Vector3.one * Random.Range(12.0f, 22.0f);

        }
    }
}
