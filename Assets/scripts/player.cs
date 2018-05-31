using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    CharacterController control;
    Transform cam;
    float rotCam;
    float forceDown;
    float upDownLook, timToSwitch;

    public Vector3[] teleportPoints;

    public float angleTrigTimer, secToSwitch;

    int currentPlace;
    public GameObject playerLight;

    // Use this for initialization
    void Start()
    {
        currentPlace = 0;
        control = GetComponent<CharacterController>();
        cam = Camera.main.transform;
        //seaBehaviorScript = seaObj.GetComponent<seaBehavior>();

    }

    // Update is called once per frame
    void Update()
    {

        Cursor.lockState = CursorLockMode.Locked;

        //float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //if(!seaBehaviorScript.stopPlayer){
        control.Move(transform.forward * Time.deltaTime * vertical * 5f);
        //control.Move(transform.right * Time.deltaTime * horizontal * 5f);
        //}

        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * 75;
        float mouseY = -Input.GetAxis("Mouse Y") * Time.deltaTime * 75;

        //if (cam.transform.localEulerAngles.x < 280 && cam.transform.localEulerAngles.x > 80)
        //    rotCam = 0;
        //else
        rotCam = mouseY + (forceDown * Time.deltaTime);


        upDownLook += rotCam;
        upDownLook = Mathf.Clamp(upDownLook, -80, 80);

        transform.Rotate(0, mouseX, 0);
        cam.localEulerAngles = new Vector3(upDownLook, 0, 0);

        float camXRot = cam.transform.localEulerAngles.x;
        if (camXRot > 180)
            camXRot -= 360;
        camXRot = Mathf.Clamp(camXRot, -80, 80);
        cam.transform.localEulerAngles = new Vector3(camXRot, cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z);

        //        Debug.Log(cam.transform.localEulerAngles.x);

        control.Move(transform.up * -Time.deltaTime * 10f);


        //stuff to force camera to look down
        if (vertical != 0)
        {
            forceDown += Time.deltaTime;
        }

        if (upDownLook > angleTrigTimer)
            timToSwitch += Time.deltaTime;

        if (timToSwitch > secToSwitch)
        {
            if (currentPlace == 0)
            {
                SwitchPlaces(1);
                playerLight.SetActive(false);
            }
            else
            {
                SwitchPlaces(0);
                playerLight.SetActive(true);
            }
        }


    }



    public void SwitchPlaces(int nextPoint)
    {
        teleportPoints[currentPlace] = transform.position;


        transform.position = teleportPoints[nextPoint];
        forceDown = 0;
        timToSwitch = 0;
        currentPlace = nextPoint;
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

}
