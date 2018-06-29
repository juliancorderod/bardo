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

        for (int i = 0; i < maxClouds * 0.35f; i++)
        {

            GameObject g = Instantiate(cloud, new Vector3(Random.Range(-distanceFromCenter, distanceFromCenter),
                                                          Random.Range(40, 50),
                                                          Random.Range(-distanceFromCenter, distanceFromCenter))
                                       + transform.position, Quaternion.identity);

            g.transform.rotation = Random.rotation;
            g.transform.localScale = Vector3.one * Random.Range(12.0f, 22.0f);

        }

        for (int i = 0; i < maxClouds * 0.3f; i++)
        {

            GameObject g = Instantiate(cloud, new Vector3(Random.Range(-distanceFromCenter * 0.5f, distanceFromCenter * 0.5f),
                                                          Random.Range(50, 70),
                                                          Random.Range(-distanceFromCenter * 0.5f, distanceFromCenter * 0.5f))
                                       + transform.position, Quaternion.identity);

            g.transform.rotation = Random.rotation;
            g.transform.localScale = Vector3.one * Random.Range(12.0f, 22.0f);

        }
        for (int i = 0; i < maxClouds * 0.25f; i++)
        {

            GameObject g = Instantiate(cloud, new Vector3(Random.Range(-distanceFromCenter * 0.25f, distanceFromCenter * 0.25f),
                                                          Random.Range(60, 80),
                                                          Random.Range(-distanceFromCenter * 0.25f, distanceFromCenter * 0.25f))
                                       + transform.position, Quaternion.identity);

            g.transform.rotation = Random.rotation;
            g.transform.localScale = Vector3.one * Random.Range(12.0f, 22.0f);

        }
        for (int i = 0; i < maxClouds * 0.10f; i++)
        {

            GameObject g = Instantiate(cloud, new Vector3(Random.Range(-distanceFromCenter * 0.05f, distanceFromCenter * 0.05f),
                                                          Random.Range(75, 80),
                                                          Random.Range(-distanceFromCenter * 0.05f, distanceFromCenter * 0.05f))
                                       + transform.position, Quaternion.identity);

            g.transform.rotation = Random.rotation;
            g.transform.localScale = Vector3.one * Random.Range(12.0f, 22.0f);

        }
    }
}
