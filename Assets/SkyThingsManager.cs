using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyThingsManager : MonoBehaviour
{

    public List<Transform> skyThings = new List<Transform>();
    public GameObject gridObj;
    public GameObject trailPrefab;
    public Transform freeTrailsParent;

    public Texture2D textToSampleFrom;

    float xMin = -400, xMax = 400, yMin = -300, yMax = 300;
    public float thingySpeed = 100, turnSpeed = 5;
    public int amountOfThingys = 25, totalTrails = 400;
    [Header("if no texture:")]
    public float noiseSize = 4f;

    List<TrailRenderer> freeTrails = new List<TrailRenderer>();



    // Use this for initialization
    void Start()
    {

        for (int i = 0; i < totalTrails; i++)
        {
            GameObject t = Instantiate(trailPrefab, freeTrailsParent);

            freeTrails.Add(t.GetComponent<TrailRenderer>());
            //t.GetComponent<TrailRenderer>().endColor = new Color(i / 10.0f, i / 10.0f, i / 10.0f);
            t.SetActive(false);
        }


        for (int i = 0; i < amountOfThingys; i++)
        {
            GameObject g = new GameObject();
            g.transform.parent = transform;
            g.transform.position = new Vector3(Random.Range(xMin * 0.8f, xMax * 0.8f), Random.Range(yMin * 0.8f, yMax * 0.8f), 495);
            g.name = "skyThing " + i;
            skyThings.Add(g.transform);

            AssignTrail(g.transform, i);
        }

        //for (int x = 0; x < 100; x++)
        //{
        //    for (int y = 0; y < 100; y++)
        //    {
        //        GameObject g = Instantiate(gridObj, new Vector3((x * 8) - 400, (y * 6) - 300, 500), Quaternion.Euler(0, 0, 0));
        //        g.transform.localScale = new Vector3(5, 5, 1);

        //        float n = GetPNoise(g.transform.position.x, g.transform.position.y);
        //        g.GetComponent<SpriteRenderer>().color = new Color(n, n, n);
        //    }
        //}
    }

    int wrapsAround;

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < skyThings.Count; i++)
        {
            float textVal = GetPTexture(skyThings[i].position.x, skyThings[i].position.y);
            float textValNext = GetPTexture(skyThings[i].position.x + skyThings[i].right.x * 3, skyThings[i].position.y + skyThings[i].right.y * 3);




            float turnVal = (textVal - textValNext);

            //float turnVal = (i + 0.5f / 10) - textValNext;


            skyThings[i].localEulerAngles += new Vector3(0, 0, turnVal * Time.deltaTime * thingySpeed * turnSpeed);
            skyThings[i].position += skyThings[i].right * Time.deltaTime * thingySpeed;
            skyThings[i].position -= skyThings[i].forward * Time.deltaTime * 1f;

            bool wrappedAround = false;
            if (Mathf.Abs(skyThings[i].position.x) > xMax)
            {
                wrappedAround = true;
            }
            if (Mathf.Abs(skyThings[i].position.y) > yMax)
            {
                wrappedAround = true;
            }

            if (wrappedAround)
            {
                wrapsAround++;
                RemoveTrail(skyThings[i]);
                skyThings[i].position = new Vector3(Random.Range(xMin * 0.8f, xMax * 0.8f), Random.Range(yMin * 0.8f, yMax * 0.8f), 495);
                AssignTrail(skyThings[i], i);
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
            freeTrails[0].endColor = new Color((index % 10) / 10.0f, (index % 10) / 10.0f, (index % 10) / 10.0f);
            freeTrails[0].Clear();
            freeTrails[0].enabled = true;
            freeTrails.Remove(freeTrails[0]);

        }
    }



    float GetPTexture(float x, float y)
    {
        if (textToSampleFrom != null)
        {
            Color pixelCol = textToSampleFrom.GetPixel(Mathf.RoundToInt(Geo.remapRange(x, xMin, xMax, 0, textToSampleFrom.width)),
                                                    Mathf.RoundToInt(Geo.remapRange(y, yMin, yMax, 0, textToSampleFrom.height)));
            float value = pixelCol.grayscale;

            return value;
        }
        else
            return GetPNoise(x, y);
    }

    float GetPNoise(float x, float y)
    {

        float noise = Mathf.PerlinNoise(Geo.remapRange(x, xMin, xMax, 0, 1) * noiseSize,
                                        Geo.remapRange(y, yMin, yMax, 0, 1) * noiseSize);

        return noise;

    }
}
