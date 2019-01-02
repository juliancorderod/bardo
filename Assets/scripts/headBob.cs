using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headBob : MonoBehaviour
{

    public float amplitude, freq, clamp;

    float yVal, lerpVal;


    // Use this for initialization
    void Start()
    {
        lerpVal = 1;

    }

    // Update is called once per frame
    void Update()
    {

        //		Debug.Log(yValWithCrouch);


        //if (Mathf.Round(Input.GetAxis("Vertical")) != 0 || Mathf.Round(Input.GetAxis("Horizontal")) != 0)
        //{

        //    lerpVal -= Time.deltaTime * 5;
        //    //fxSound.volume += Time.deltaTime * 1.25f;


        //}
        //else
        //{
        //    lerpVal += Time.deltaTime * 5f;

        //}

        ////Debug.Log(lerpVal);

        //yVal = Mathf.Lerp(0.75f + (Mathf.Sin(Time.time * freq) / amplitude), 0.75f, lerpVal);




        //transform.localPosition = new Vector3(transform.localPosition.x, yVal, transform.localPosition.z);

        //lerpVal = Mathf.Clamp01(lerpVal);
        //yVal = Mathf.Clamp(yVal, clamp, 100f);




    }

    void LateUpdate()
    {


        float waveY = Mathf.Sin(Time.time * freq * 0.1f) / amplitude * 0.3f;
        float waveX = Mathf.Sin(Time.time * freq * 0.01f) / amplitude * 0.25f;

        if (Mathf.Round(Input.GetAxis("Vertical")) == 0 && Mathf.Round(Input.GetAxis("Horizontal")) == 0)
            transform.localPosition = new Vector3(waveX, transform.localPosition.y + waveY, transform.localPosition.z);

        //print(waveX);
    }
}
