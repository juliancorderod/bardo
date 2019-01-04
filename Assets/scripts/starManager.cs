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
                s.transform.localScale = Vector3.one * Random.Range(0.25f, maxSize);
                s.transform.parent = transform;
                s.transform.LookAt(ms.transform);
                //s.GetComponent<SpriteRenderer>().color = Color.white * 100;
                ms.stars.Add(s);
                ms.originalStarSize.Add(s.transform.localScale.magnitude);
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
