using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CityBuilder : MonoBehaviour
{

    public GameObject building, top1, top2, topTall, street1;
    public float maxBuildingsSide;

    float sizeX, sizeZ, sizeY, posY;

    //bool placedCentralBuilding = false;

    public NewPlayer p;

    Vector3 meshSize;

    int groundLayer = 1 << 8;

    public Transform centralBuild;

    public MuerteSubitaManager m;

    public GameObject lampPost;

    public bool hasFinished;

    // Use this for initialization
    void Awake()
    {
        meshSize = building.transform.GetChild(0).transform.GetChild(3).GetComponent<Renderer>().bounds.size;

        for (int x = 0; x < maxBuildingsSide; x++)
        {
            for (int z = 0; z < maxBuildingsSide; z++)
            {

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
                g.transform.localPosition = new Vector3(x * meshSize.x, 800, z * meshSize.z);

                g.transform.localPosition -= new Vector3((maxBuildingsSide * meshSize.x) / 2, 0, (maxBuildingsSide * meshSize.z) / 2);//esto les centra

                RaycastHit hit;
                Ray groundCheckRay = new Ray(g.transform.position, Vector3.down);
                Physics.Raycast(groundCheckRay, out hit, 1000f, groundLayer);

                if (hit.collider != null)
                {
                    posY = hit.point.y;
                }

                g.transform.position = new Vector3(g.transform.position.x, posY, g.transform.position.z);


                if (x % 5 == 0 && z % 4 == 0)//roads
                {

                    if (centralBuild.position.z < g.transform.position.z)
                    {

                        if (Random.Range(0, 10) < 9)
                        {

                            GameObject t = Instantiate(lampPost, transform);
                            t.transform.position = g.transform.position;
                            if (Random.Range(0, 10) > 5)
                                t.transform.eulerAngles = new Vector3(0, 45, 0);
                            else
                                t.transform.eulerAngles = new Vector3(0, 225, 0);
                        }
                    }


                    Destroy(g);
                    destroyed = true;
                }
                if (z % 4 == 0)
                {

                    if (x % 5 != 0 && g.transform.position.z > centralBuild.position.z + 10)
                    {
                        if (Random.Range(0, 10) > 2)
                        {
                            GameObject street = Instantiate(street1, transform);
                            street.transform.position = g.transform.position;
                        }

                    }


                    Destroy(g);
                    destroyed = true;

                }

                if (x % 5 == 0)//roads
                {


                    //if (centralBuild.position.z < g.transform.position.z)
                    //{
                    //    if (Random.Range(0, 10) > 5)
                    //    {
                    //        GameObject t = Instantiate(lampPost, transform);
                    //        t.transform.position = g.transform.position + Vector3.up * 4;

                    //    }
                    //}


                    Destroy(g);
                    destroyed = true;

                }




                if (x % 10 == 2 || x % 10 == 7)
                {
                    if (z % 4 == 2)
                    {
                        g.transform.localScale = new Vector3(2.3f, 0.1f, 1.5f);
                        g.transform.localPosition += new Vector3(5, 1, 0);
                        //g.GetComponent<MeshRenderer>().material.color = Color.red;
                        dontDestroy = true;
                        Destroy(g);
                        destroyed = true;
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
                            //if (Random.Range(0, 10) > 6)
                            //{
                            //    GameObject t = Instantiate(lampPost, transform);
                            //    t.transform.position = g.transform.position;
                            //    t.transform.eulerAngles = new Vector3(0, Random.Range(0, 359), 0);
                            //}

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
                    if (Mathf.Abs(Vector3.Distance(g.transform.position, centralBuild.position)) < 35)
                    {


                        Destroy(g);
                        destroyed = true;
                    }
                }

                //if (!placedCentralBuilding)// && !destroyed)
                //{

                //    //if (x  maxBuildingsSide / 3f && x < (maxBuildingsSide * 2) / 3 &&
                //    //                              z < maxBuildingsSide / 8f)// && z < (maxBuildingsSide * 2) / 3)
                //    //{
                //    if (z == 0 && x == Mathf.RoundToInt(maxBuildingsSide / 2) - 2)
                //    {
                //        //                        Debug.Log(x + "," + z);

                //        //if (Random.Range(0, 100) > 70)
                //        //{
                //        Vector3 pos = g.transform.localPosition;


                //        Destroy(g);
                //        destroyed = true;

                //        GameObject c = Instantiate(centralBuilding, transform);
                //        c.name = "centralBuilding";
                //        c.transform.localPosition = pos;
                //        //c.transform.localScale = new Vector3(10, 10, 10);
                //        c.GetComponent<MeshRenderer>().material.color = Color.black;

                //        p.muerteSubitaTrigger = c.transform.GetChild(0);

                //        placedCentralBuilding = true;
                //        //}

                //    }
                //}

                if (!destroyed && !dontDestroy)
                {
                    GameObject highestBuild = null;
                    bool isTallest = false;

                    if (sizeY <= 30)
                        highestBuild = g;


                    if (sizeY > 30)
                    {
                        GameObject gg = Instantiate(building, transform);
                        gg.transform.localPosition = new Vector3(g.transform.localPosition.x,
                                                                 g.transform.localPosition.y + meshSize.y, g.transform.localPosition.z);
                        highestBuild = gg;
                    }
                    if (sizeY > 40)
                    {
                        GameObject ggg = Instantiate(building, transform);
                        ggg.transform.localPosition = new Vector3(g.transform.localPosition.x,
                                                                  g.transform.localPosition.y + meshSize.y * 2, g.transform.localPosition.z);
                        highestBuild = ggg;
                    }
                    if (sizeY > 50)
                    {
                        GameObject gggg = Instantiate(building, transform);
                        gggg.transform.localPosition = new Vector3(g.transform.localPosition.x,
                                                                   g.transform.localPosition.y + meshSize.y * 3, g.transform.localPosition.z);
                        highestBuild = gggg;
                    }
                    if (sizeY > 60)
                    {
                        GameObject ggggg = Instantiate(building, transform);
                        ggggg.transform.localPosition = new Vector3(g.transform.localPosition.x,
                                                                   g.transform.localPosition.y + meshSize.y * 4, g.transform.localPosition.z);
                        highestBuild = ggggg;
                    }
                    if (sizeY > 70)
                    {
                        GameObject gggggg = Instantiate(building, transform);
                        gggggg.transform.localPosition = new Vector3(g.transform.localPosition.x,
                                                                   g.transform.localPosition.y + meshSize.y * 5, g.transform.localPosition.z);
                        highestBuild = gggggg;
                        isTallest = true;
                    }
                    if (sizeY > 80)
                    {
                        GameObject ggggggg = Instantiate(building, transform);
                        ggggggg.transform.localPosition = new Vector3(g.transform.localPosition.x,
                                                                   g.transform.localPosition.y + meshSize.y * 6, g.transform.localPosition.z);
                        highestBuild = ggggggg;

                        isTallest = true;
                    }



                    GameObject top = null;

                    if (isTallest)
                    {

                        top = Instantiate(topTall, transform);

                    }
                    else
                    {
                        if (Random.Range(0, 10) > 5)
                            top = Instantiate(top1, transform);
                        else
                            top = Instantiate(top2, transform);
                    }

                    top.transform.localPosition = new Vector3(g.transform.localPosition.x,
                                                              highestBuild.transform.localPosition.y + meshSize.y * 0.846f, g.transform.localPosition.z);

                    top.transform.localScale = new Vector3(top.transform.localScale.x,
                    top.transform.localScale.y * Random.Range(0.7f, 1.5f),
                    top.transform.localScale.z);

                    if (Random.Range(0, 10) > 5)
                        top.transform.eulerAngles = new Vector3(0, 180, 0);
                }





            }

        }

        m.enabled = true;
        m.streetLightColor = lampPost.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.color;


    }




}
