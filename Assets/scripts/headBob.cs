using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headBob : MonoBehaviour
{

    public float amplitude, freq, clamp;

    float yVal, lerpVal;

    public AudioSource playerSound;
    //public AudioSource fxSound;

    public AudioClip step1, step2, step3, step4, splash1, splash2, splash3, splash4;

    bool stepCanPlay = false;

    public GameObject seaObj;
    //	seaBehavior seaScript;


    //	bool setF = true;
    //	float f;

    // Use this for initialization
    void Start()
    {
        lerpVal = 1;
        //seaScript = seaObj.GetComponent<seaBehavior>();

    }

    // Update is called once per frame
    void Update()
    {

        //		Debug.Log(yValWithCrouch);


        if (Mathf.Round(Input.GetAxis("Vertical")) != 0)
        {

            lerpVal -= Time.deltaTime * 5;
            //fxSound.volume += Time.deltaTime * 1.25f;


        }
        else
        {
            lerpVal += Time.deltaTime * 5;

        }



        yVal = Mathf.Lerp(0.75f + (Mathf.Sin(Time.time * freq) / amplitude), 0.75f, lerpVal);




        transform.localPosition = new Vector3(transform.localPosition.x, yVal, transform.localPosition.z);

        lerpVal = Mathf.Clamp01(lerpVal);
        yVal = Mathf.Clamp(yVal, clamp, 100f);



        if (yVal == 0.66f && stepCanPlay)
        {

            playStepSound();
            stepCanPlay = false;
        }
        if (yVal > 0.75f)
        {
            stepCanPlay = true;
        }






    }

    void playStepSound()
    {

        int randNum = Random.Range(1, 5);

        //if (!seaScript.inSea)
        //{
        //    if (randNum == 1)
        //    {
        //        playerSound.clip = step1;
        //        playerSound.Play();
        //    }
        //    if (randNum == 2)
        //    {
        //        playerSound.clip = step2;
        //        playerSound.Play();
        //    }
        //    if (randNum == 3)
        //    {
        //        playerSound.clip = step3;
        //        playerSound.Play();
        //    }
        //    if (randNum == 4)
        //    {
        //        playerSound.clip = step4;
        //        playerSound.Play();
        //    }
        //}
        //else
        //{
        if (randNum == 1)
        {
            playerSound.clip = splash1;
            playerSound.Play();
        }
        if (randNum == 2)
        {
            playerSound.clip = splash2;
            playerSound.Play();
        }
        if (randNum == 3)
        {
            playerSound.clip = splash3;
            playerSound.Play();
        }
        if (randNum == 4)
        {
            playerSound.clip = splash4;
            playerSound.Play();
        }

        //}
    }

    void LateUpdate()
    {


        float waveY = Mathf.Sin(Time.time * freq * 0.1f) / amplitude * 0.3f;
        float waveX = Mathf.Sin(Time.time * freq * 0.01f) / amplitude * 0.25f;

        if (Mathf.Round(Input.GetAxis("Vertical")) == 0 && Mathf.Round(Input.GetAxis("Horizontal")) == 0)
            transform.localPosition = new Vector3(waveX, 0.75f + waveY, transform.localPosition.z);

        //print(waveX);
    }
}
