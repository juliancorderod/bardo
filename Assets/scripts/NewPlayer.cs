using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayer : MonoBehaviour
{
    public Transform camHolder;

    float mouseX, mouseY;

    public float moveSpeed, slowDownSpeed, maxSpeed;
    Vector3 acceleration, speed;

    public float camHeightUp, camHeightDown, camHeightFloor, descendSpeed;
    float groundHeight, yPos, skyLerp = 1, descendLerp;

    Vector3 camRotJuice;
    public float camRotReturnSpeed, maxCamRotSpeed, camRotLerp, maxRotTotal;

    RaycastHit hit;
    int groundLayer = 1 << 8;

    public Light mountainLight, cityLight, floorLight;
    float mountainLightIntensity, cityLightIntensity;
    enum Location
    {
        NOWHERE,
        MOUNTAINS,
        CITY,
        WEIRDROOM
    }
    Location location;

    float sineWaveForYRot;

    bool onFloor, descending, ascending, onSky;

    float upDownLook, lrLook;
    public float lookSpeed;

    public AudioSource overWorldSong, ambientAudio;
    public AudioClip mountainSound, citySound, wierdRoomSound;

    public bool onIOS;
    public float iosAdjustmentSpeed;

    bool clicked = false;// hasMovedTouch = false, hasStationaryTouch = false;

    public TextMesh Text;

    //Vector3 oldMousePos, newMousePos;

    public bool inSongArea;

    // Use this for initialization
    void Start()
    {
        mountainLightIntensity = mountainLight.intensity;
        cityLightIntensity = cityLight.intensity;

        ascending = true;

#if UNITY_IOS
        onIOS = true;
#endif


    }

    void Update()
    {

        //debugText.text = mouseX.ToString("F4") + "," + mouseY.ToString("F4");

        Cursor.lockState = CursorLockMode.Locked;

        if (!onIOS)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                clicked = true;

            }
            else
                clicked = false;

            if (Input.GetKeyDown(KeyCode.Period))
                moveSpeed *= 1.5f;
            if (Input.GetKeyDown(KeyCode.Comma))
                moveSpeed /= 1.5f;


        }
        else
        {
            //if (Input.touchCount > 0)
            //{

            //    if (Input.GetTouch(0).deltaPosition.magnitude > 0.01f)
            //        hasMovedTouch = true;

            //    if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            //        hasStationaryTouch = true;

            //    if (Input.GetTouch(0).phase != TouchPhase.Began && Input.GetTouch(0).phase != TouchPhase.Ended)
            //    {
            //        if (!hasMovedTouch && hasStationaryTouch)
            //            clicked = true;
            //        Debug.Log(Input.GetTouch(0).deltaPosition.magnitude + "|||" + hasMovedTouch);
            //    }

            //}
            //else
            //{
            //    hasMovedTouch = false;
            //    hasStationaryTouch = false;
            //}
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                clicked = true;

            }
            else
                clicked = false;
        }


        if (clicked)
        {
            if (onSky)
            {
                descending = true;
                onSky = false;
            }

            if (onFloor)
            {
                ascending = true;
                onFloor = false;
            }
            clicked = false;
        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!onFloor)
        {
            FlyMovement();
        }
        else
            FPCamera();

        if (descending && descendLerp < 0.01f)
        {
            onFloor = true;
            descending = false;
        }


        if (ascending && descendLerp > 0.99f)
        {
            onSky = true;
            ascending = false;
        }




        if (!onIOS)
        {

            mouseX = Input.GetAxis("Mouse X") * Time.fixedDeltaTime * moveSpeed;
            mouseY = Input.GetAxis("Mouse Y") * Time.fixedDeltaTime * moveSpeed;
        }
        else
        {

            if (Input.touchCount > 0)
            {
                mouseX = Input.GetTouch(0).deltaPosition.x * Time.fixedDeltaTime * moveSpeed * iosAdjustmentSpeed;
                mouseY = Input.GetTouch(0).deltaPosition.y * Time.fixedDeltaTime * moveSpeed * iosAdjustmentSpeed;
            }
            else
            {
                mouseX = 0;
                mouseY = 0;
            }
        }



        //if (Input.touchCount > 0)
        //    Debug.Log(Input.GetTouch(0).deltaPosition);
        //else
        //Debug.Log(Input.GetAxis("Mouse X") + "," + Input.GetAxis("Mouse Y"));



        //MUSIC DESCENCION STUFF
        overWorldSong.volume = descendLerp;
        ambientAudio.volume = 1 - descendLerp;




        if (inSongArea)
            Text.color += new Color(0, 0, 0, Time.deltaTime * 0.2f);
        else
            Text.color -= new Color(0, 0, 0, Time.deltaTime * 1f);
    }



    void FlyMovement()
    {
        //------------------------------------------ DIRECT MOVEMENT ------------------------------------------


        acceleration = new Vector3(mouseX, 0, mouseY);
        //if (!descending)
        speed += acceleration;

        speed = Vector3.Lerp(speed, Vector3.zero, slowDownSpeed);
        speed = Vector3.ClampMagnitude(speed, maxSpeed);

        //transform.position -= speed;


        transform.Translate(-speed.x, 0, -speed.z, transform);



        //------------------------------------------ HEIGHT & DESCENT ------------------------------------------

        //Where is the ground?
        Ray groundCheckRay = new Ray(transform.position, Vector3.down);
        Physics.Raycast(groundCheckRay, out hit, 1000f, groundLayer);

        if (hit.collider != null)
            groundHeight = hit.point.y;
        else
        {
            groundHeight = 0;
            Debug.Log("no ground!");
        }

        //descension: QUE EL BAJAR Y SUBIR SE SIENTA MUCHO MEJORRRRR
        if (descending)
        {

            descendLerp -= Time.fixedDeltaTime * descendSpeed;


            ascending = false;

        }
        if (ascending)
        {

            descendLerp += Time.fixedDeltaTime * descendSpeed;

            descending = false;

        }



        descendLerp = Mathf.Clamp01(descendLerp);

        skyLerp = Mathf.Sin(Time.time * 0.2f) * 0.5f + 0.5f;
        yPos = Mathf.Lerp(camHeightFloor, Mathf.Lerp(camHeightDown, camHeightUp, skyLerp), descendLerp) + groundHeight;

        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
        camHolder.localEulerAngles = new Vector3(Mathf.LerpAngle(0, 90, descendLerp), 0, 0);


        //------------------------------------------ CAMERA ROTATION JUICE ------------------------------------------
        camRotJuice += new Vector3(-acceleration.z * 1000, acceleration.x * 1000, 0) * Time.fixedDeltaTime;
        camRotJuice = Vector3.Lerp(camRotJuice, Vector3.zero, camRotReturnSpeed);
        camRotJuice = Vector3.ClampMagnitude(camRotJuice, maxCamRotSpeed);

        //if (!descending)
        //{
        Camera.main.transform.localEulerAngles += camRotJuice;


        Camera.main.transform.localEulerAngles = new Vector3(Mathf.LerpAngle(Camera.main.transform.localEulerAngles.x, 0, camRotLerp),
                                                             Mathf.LerpAngle(Camera.main.transform.localEulerAngles.y, 0, camRotLerp),
                                                             0);
        //}
        //for the clamping:

        if (onSky)
        {
            if (Camera.main.transform.localEulerAngles.x < 180)
                Camera.main.transform.localEulerAngles = new Vector3(Mathf.Clamp(Camera.main.transform.localEulerAngles.x, 0, maxRotTotal),
                                                                    Camera.main.transform.localEulerAngles.y,
                                                                    Camera.main.transform.localEulerAngles.z);
            else
                Camera.main.transform.localEulerAngles = new Vector3(Mathf.Clamp(Camera.main.transform.localEulerAngles.x, 360 - maxRotTotal, 360),
                                                                    Camera.main.transform.localEulerAngles.y,
                                                                    Camera.main.transform.localEulerAngles.z);
            if (Camera.main.transform.localEulerAngles.y < 180)
                Camera.main.transform.localEulerAngles = new Vector3(Camera.main.transform.localEulerAngles.x,
                                                                    Mathf.Clamp(Camera.main.transform.localEulerAngles.y, 0, maxRotTotal),
                                                                    Camera.main.transform.localEulerAngles.z);
            else
                Camera.main.transform.localEulerAngles = new Vector3(Camera.main.transform.localEulerAngles.x,
                                                                     Mathf.Clamp(Camera.main.transform.localEulerAngles.y, 360 - maxRotTotal, 360),
                                                                     Camera.main.transform.localEulerAngles.z);

        }



        //------------------------------------------ Y ROTATION ------------------------------------------

        sineWaveForYRot = Mathf.Sin(Time.time * 0.2f) * 0.5f;


        transform.eulerAngles += Vector3.Lerp(Vector3.zero, new Vector3(0, (speed.x + speed.z) * sineWaveForYRot, 0), 0.5f);




        //------------------------------------------ WRAP AROUND MAP ------------------------------------------
        wrapAround();

        //------------------------------------------ LOCATION MANAGER ------------------------------------------
        if (hit.collider != null)
        {
            if (hit.collider.tag == "mountains")
                location = Location.MOUNTAINS;

            if (hit.collider.tag == "city")
                location = Location.CITY;
            if (hit.collider.tag == "weirdRoom")
                location = Location.WEIRDROOM;

        }
        else
        {
            location = Location.NOWHERE;
        }

        switch (location)
        {

            case Location.NOWHERE:

                cityLight.intensity -= Time.fixedDeltaTime * 2 * cityLightIntensity;
                mountainLight.intensity -= Time.fixedDeltaTime * 2 * mountainLightIntensity;

                break;
            case Location.MOUNTAINS:

                cityLight.intensity -= Time.fixedDeltaTime * 2 * cityLightIntensity;
                mountainLight.intensity += Time.fixedDeltaTime * 0.2f * mountainLightIntensity;

                if (floorLight.intensity < 4)
                    floorLight.intensity += Time.fixedDeltaTime * 0.5f;

                ambientAudio.clip = mountainSound;
                if (!ambientAudio.isPlaying)
                    ambientAudio.Play();

                break;
            case Location.CITY:

                cityLight.intensity += Time.fixedDeltaTime * 0.2f * cityLightIntensity;
                mountainLight.intensity -= Time.fixedDeltaTime * 2 * mountainLightIntensity;

                if (floorLight.intensity > 1)
                    floorLight.intensity -= Time.fixedDeltaTime * 1f;

                ambientAudio.clip = citySound;
                if (!ambientAudio.isPlaying)
                    ambientAudio.Play();

                break;

            case Location.WEIRDROOM:



                cityLight.intensity += Time.fixedDeltaTime * 0.05f * cityLightIntensity;
                mountainLight.intensity -= Time.fixedDeltaTime * 2 * mountainLightIntensity;

                if (floorLight.intensity > 1)
                    floorLight.intensity -= Time.fixedDeltaTime * 1f;

                ambientAudio.clip = wierdRoomSound;
                if (!ambientAudio.isPlaying)
                    ambientAudio.Play();

                break;
        }

        cityLight.intensity = Mathf.Clamp(cityLight.intensity, 0, cityLightIntensity);
        mountainLight.intensity = Mathf.Clamp(mountainLight.intensity, 0, mountainLightIntensity);



    }

    void FPCamera()
    {
        //UPDOWN:


        upDownLook = mouseY * lookSpeed;
        //upDownLook = Mathf.Clamp(upDownLook, -80, 80);


        Camera.main.transform.localEulerAngles += new Vector3(-upDownLook, 0, 0);

        if (Camera.main.transform.localEulerAngles.x < 280 && Camera.main.transform.localEulerAngles.x > 180)
            Camera.main.transform.localEulerAngles = new Vector3(280, 0, 0);
        if (Camera.main.transform.localEulerAngles.x > 80 && Camera.main.transform.localEulerAngles.x < 180)
            Camera.main.transform.localEulerAngles = new Vector3(80, 0, 0);



        //upDownLookClamp = Camera.main.transform.localEulerAngles.x;
        //upDownLookClamp += 90;
        //upDownLookClamp = Mathf.Clamp(upDownLookClamp, 5, 175);
        //upDownLookClamp -= 90;
        //Camera.main.transform.localEulerAngles = new Vector3(upDownLookClamp,
        //Camera.main.transform.localEulerAngles.y,
        //Camera.main.transform.localEulerAngles.z);

        //LEFTRIGHT:
        lrLook = mouseX * lookSpeed;

        transform.eulerAngles += new Vector3(transform.eulerAngles.x, lrLook, transform.eulerAngles.z);
    }

    void wrapAround()
    {
        //if (transform.position.x > 225)
        //    transform.position = new Vector3(-595, transform.position.y, transform.position.z);
        //if (transform.position.x < -600)
        //    transform.position = new Vector3(220, transform.position.y, transform.position.z);
        //if (transform.position.z > 480)
        //    transform.position = new Vector3(transform.position.x, transform.position.y, -145);
        //if (transform.position.z < -150)
        //transform.position = new Vector3(transform.position.x, transform.position.y, 475);


        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -520, 175),
                                         transform.position.y,
                                         Mathf.Clamp(transform.position.z, -60, 420));
    }



    private void OnTriggerEnter(Collider other)
    {
        inSongArea = true;
        //        Debug.Log("in");
    }

    private void OnTriggerExit(Collider other)
    {
        inSongArea = false;

    }

}
