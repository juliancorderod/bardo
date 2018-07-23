using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starManager : MonoBehaviour
{

    public GameObject starObj;
    public float starRange, starNum, maxSize;
    public MuerteSubitaManager ms;

    // Use this for initialization
    void Start()
    {


        for (int i = 0; i < starNum; i++)
        {
            Vector3 randOnSphere = Random.onUnitSphere * starRange;
            if (randOnSphere.y > 0)
            {
                GameObject s = Instantiate(starObj, randOnSphere, Quaternion.identity);
                s.transform.localScale = Vector3.one * 2; //Random.Range(0.5f, maxSize);
                s.transform.parent = transform;
                s.transform.LookAt(ms.transform);

                ms.stars.Add(s);
            }
            else
                i--;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
