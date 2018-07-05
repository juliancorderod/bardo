using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CityBuilder : MonoBehaviour
{

    public GameObject building, centralBuilding;
    public float maxBuildingsSide;

    float sizeX, sizeZ, sizeY;

    bool placedCentralBuilding = false;

    public NewPlayer p;

    Vector3 meshSize;



    // Use this for initialization
    void Start()
    {
        meshSize = building.transform.GetChild(0).transform.GetChild(3).GetComponent<Renderer>().bounds.size;

        for (int x = 0; x < maxBuildingsSide; x++)
        {
            for (int z = 0; z < maxBuildingsSide; z++)
            {
                //if (x % 4 == 0)
                //    spaceBetweenBuildingsX = 1;
                //else
                //    spaceBetweenBuildingsX = 0;

                //if (y % 3 == 0)
                //    spaceBetweenBuildingsY = 2;
                //else
                //spaceBetweenBuildingsY = 0;

                bool dontDestroy = false;
                bool destroyed = false;

                sizeZ = Random.Range(9.5f, 12.0f);
                sizeX = Random.Range(9.5f, 12.0f);

                if (x < maxBuildingsSide / 2)
                    sizeY = Random.Range(15.0f, 25.0f) * (x / (maxBuildingsSide / 4)) * 2.5f + 0.5f;
                else
                    sizeY = Random.Range(15.0f, 25.0f) * ((maxBuildingsSide - x) / (maxBuildingsSide / 4)) * 2.5f + 0.5f;

                if (z < maxBuildingsSide / 2)
                    sizeY *= z / (maxBuildingsSide / 2);
                else
                    sizeY *= (maxBuildingsSide - z) / (maxBuildingsSide / 2);



                GameObject g = Instantiate(building, transform);
                g.transform.localPosition = new Vector3(x * meshSize.x, 0, z * meshSize.z);




                //g.transform.localScale = new Vector3(sizeX, sizeY, sizeZ);







                g.transform.localPosition -= new Vector3((maxBuildingsSide * 10) / 2, 1, (maxBuildingsSide * 10) / 2);//esto les centra

                if (x % 5 == 0 || z % 4 == 0)
                {
                    g.transform.localScale = new Vector3(1, 0.1f, 1);
                    g.transform.localPosition += new Vector3(0, 0, 0);
                    //g.GetComponent<MeshRenderer>().material.color = Color.black;
                    dontDestroy = true;

                }




                if (x % 10 == 2 || x % 10 == 7)
                {
                    if (z % 4 == 2)
                    {
                        g.transform.localScale = new Vector3(2.3f, 0.1f, 1.5f);
                        g.transform.localPosition += new Vector3(5, 1, 0);
                        //g.GetComponent<MeshRenderer>().material.color = Color.red;
                        dontDestroy = true;
                    }
                }
                if (!dontDestroy)//poner arbolitos o huevadas en vez de solo destruir
                {
                    if (x % 10 == 3 || x % 10 == 8)
                    {
                        if (z % 4 == 2)
                        {
                            Destroy(g);
                            destroyed = true;
                        }
                    }

                    if (x < maxBuildingsSide / 3)
                    {
                        if (Random.Range(0, maxBuildingsSide / 3) > x)
                        {
                            Destroy(g);
                            destroyed = true;
                        }
                    }

                    if (x > (maxBuildingsSide * 2) / 3)
                    {
                        if (Random.Range((maxBuildingsSide * 2) / 3, maxBuildingsSide) < x)
                        {
                            Destroy(g);
                            destroyed = true;

                        }
                    }
                    if (z < maxBuildingsSide / 3)
                    {
                        if (Random.Range(0, maxBuildingsSide / 3) > z)
                        {
                            Destroy(g);
                            destroyed = true;
                        }
                    }

                    if (z > (maxBuildingsSide * 2) / 3)
                    {
                        if (Random.Range((maxBuildingsSide * 2) / 3, maxBuildingsSide) < z)
                        {
                            Destroy(g);
                            destroyed = true;

                        }
                    }
                }

                if (!placedCentralBuilding && !destroyed)
                {
                    if (x > maxBuildingsSide / 3f && x < (maxBuildingsSide * 2) / 3 &&
                        z > maxBuildingsSide / 3f && z < (maxBuildingsSide * 2) / 3)
                    {

                        if (Random.Range(0, 100) > 95)
                        {
                            Vector3 pos = g.transform.localPosition;


                            Destroy(g);
                            destroyed = true;

                            GameObject c = Instantiate(centralBuilding, transform);
                            c.name = "centralBuilding";
                            c.transform.localPosition = pos;
                            c.transform.localScale = new Vector3(10, 50, 10);
                            c.GetComponent<MeshRenderer>().material.color = Color.yellow;

                            p.muerteSubitaTrigger = c.transform.GetChild(0);

                            placedCentralBuilding = true;
                        }

                    }
                }

                if (!destroyed && !dontDestroy)
                {
                    if (sizeY > 35)
                    {
                        GameObject gg = Instantiate(building, transform);
                        gg.transform.localPosition = new Vector3(g.transform.localPosition.x, meshSize.y, g.transform.localPosition.z);
                    }
                    if (sizeY > 55)
                    {
                        GameObject ggg = Instantiate(building, transform);
                        ggg.transform.localPosition = new Vector3(g.transform.localPosition.x, meshSize.y * 2, g.transform.localPosition.z);

                    }
                    if (sizeY > 75)
                    {
                        GameObject gggg = Instantiate(building, transform);
                        gggg.transform.localPosition = new Vector3(g.transform.localPosition.x, meshSize.y * 3, g.transform.localPosition.z);

                    }
                }


            }

        }

        if (!placedCentralBuilding)
            Debug.Log("noooo que verga");//poner un fix para esto!!
    }


}
