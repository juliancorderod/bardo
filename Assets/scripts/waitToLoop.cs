using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waitToLoop : MonoBehaviour
{

    Animator a;

    public float minWait, maxWait;
    float randWait;

    public string animationStateName;
    bool setRandWait;


    // Use this for initialization
    void Start()
    {
        a = GetComponent<Animator>();

        randWait = Random.Range(minWait, maxWait);
        a.speed = 1 / randWait;

    }

    // Update is called once per frame
    void Update()
    {


        if (a.GetNextAnimatorStateInfo(0).IsName("empty"))
        {
            a.speed = 1 / randWait;
            setRandWait = false;
        }
        if (a.GetNextAnimatorStateInfo(0).IsName(animationStateName))
        {
            a.speed = 1;
            if (!setRandWait)
            {
                randWait = Random.Range(minWait, maxWait);
                setRandWait = true;
            }
        }


        //Debug.Log(randWait);
    }
}
