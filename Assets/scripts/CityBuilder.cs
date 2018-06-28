using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class CityBuilder : MonoBehaviour
{

    public GameObject building;
    public float maxBuildingsSide;
    float spaceBetweenBuildingsX, spaceBetweenBuildingsY;

    float sizeX, sizeZ;
    float accumulatedPositionX, accumulatedPositionZ;

    // Use this for initialization
    void Start()
    {

        for (int x = 0; x < maxBuildingsSide; x++)
        {
            for (int y = 0; y < maxBuildingsSide; y++)
            {
                //if (x % 4 == 0)
                //    spaceBetweenBuildingsX = 1;
                //else
                //    spaceBetweenBuildingsX = 0;

                //if (y % 3 == 0)
                //    spaceBetweenBuildingsY = 2;
                //else
                //spaceBetweenBuildingsY = 0;



                sizeZ = Random.Range(8.0f, 12.0f);
                sizeX = Random.Range(8.0f, 12.0f);
                accumulatedPositionZ += sizeZ;



                Debug.Log(spaceBetweenBuildingsX);
                GameObject g = Instantiate(building, transform);
                g.transform.localPosition = new Vector3(x * 10, -5, y * 10);



                g.transform.localScale = new Vector3(sizeX, Random.Range(8.0f, 25.0f) * (x / maxBuildingsSide) * 3 + 0.5f, sizeZ);

                //if (g.transform.localPosition.x < 150 && g.transform.localPosition.z < 120)
                //  g.transform.localScale = new Vector3(g.transform.localScale.x, g.transform.localScale.y * 4, g.transform.localScale.z);


                g.transform.localPosition -= new Vector3((maxBuildingsSide * 10) / 2, 0, (maxBuildingsSide * 10) / 2);//esto les centra

                if (x % 5 == 0)
                    Destroy(g);


                if (y % 4 == 0)
                    Destroy(g);



            }
            accumulatedPositionZ = 0;
        }


    }



    // Update is called once per frame
    void Update()
    {

    }
}
