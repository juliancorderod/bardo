using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBuilder : MonoBehaviour
{

    public GameObject building;
    public float maxBuildingsSide;
    float spaceBetweenBuildingsX, spaceBetweenBuildingsY;

    // Use this for initialization
    void Start()
    {

        for (int x = 0; x < maxBuildingsSide; x++)
        {
            for (int y = 0; y < maxBuildingsSide; y++)
            {
                if (x % 4 == 0)
                    spaceBetweenBuildingsX = 1;
                else
                    spaceBetweenBuildingsX = 0;

                if (y % 3 == 0)
                    spaceBetweenBuildingsY = 2;
                else
                    spaceBetweenBuildingsY = 0;


                GameObject g = Instantiate(building, new Vector3(x * (10f + spaceBetweenBuildingsX), 0, y * (10f + spaceBetweenBuildingsY)), Quaternion.identity);

                g.transform.localScale = new Vector3(10, Random.Range(4.0f, 20.0f), 10);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
