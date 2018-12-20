using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{

    public float yRotSpeed;
    float xRotSpeed, zRotSpeed;

    public float timeToResetAvg;
    float resetTimer, resetTime;
    bool resetNow = true;

    int[] functions;
    int totalFunctions;

    public Transform futuroPoint;

    // Use this for initialization
    void Start()
    {

        functions = new int[3];

        totalFunctions = 3;

    }

    // Update is called once per frame
    void Update()
    {




        if (resetNow)
        {
            //for (int i = 0; i < functions.Length; i++)
            //{
            //    functions[i] = 0;
            //}
            //for (int i = 0; i < functions.Length; i++)
            //{
            //    functions[i] = Random.Range(1, totalFunctions);

            //}

            yRotSpeed *= Random.Range(-2.1f, -0.5f);

            yRotSpeed = Mathf.Clamp(yRotSpeed, -10, 10);
            xRotSpeed = yRotSpeed * Random.Range(-2.5f, 2.5f);
            zRotSpeed = yRotSpeed * Random.Range(-2.5f, 2.5f);


            resetTimer = 0;
            resetTime = Random.Range(timeToResetAvg - 5, timeToResetAvg + 5);
            resetNow = false;
        }

        if (!resetNow)
        {
            if (resetTimer < resetTime)
                resetTimer += Time.deltaTime;
            else
                resetNow = true;

        }

        transform.eulerAngles += new Vector3(Mathf.Sin(Time.time) * Time.deltaTime * xRotSpeed,
                                              Time.deltaTime * yRotSpeed,
                                             Mathf.Sin(Time.time) * Time.deltaTime * zRotSpeed);

        transform.localScale += new Vector3(Mathf.Sin(Time.time) * Time.deltaTime * xRotSpeed,
                                              Time.deltaTime * yRotSpeed * 0.1f,
                                             Mathf.Sin(Time.time) * Time.deltaTime * zRotSpeed) * 0.3f; ;

        transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x, 1, 10),
                                           Mathf.Clamp(transform.localScale.y, 0.1f, 1.3f), Mathf.Clamp(transform.localScale.z, 1, 10));

        transform.RotateAround(futuroPoint.position, Vector3.up, -0.2f);



    }
}
