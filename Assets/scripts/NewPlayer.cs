using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPlayer : MonoBehaviour
{
    public ComoJugarMenu comoJugarMenu;
    public Text clickPaBajar;
    public Transform camHolder;

    [HideInInspector]
    public float mouseX, mouseY;
    float mouseSensitivity;

    public float moveSpeed, slowDownSpeed, maxSpeed;
    float slowDownSpeedOriginal;
    Vector3 acceleration, speed;

    public float camHeightUp, camHeightDown, camHeightFloor, descendSpeed;
    float groundHeight, yPos, skyLerp = 1, descendLerp, normalCamHeightFloor;

    Vector3 camRotJuice;
    public float camRotReturnSpeed, maxCamRotSpeed, camRotLerp, maxRotTotal;

    RaycastHit hit;
    int groundLayer = 1 << 8 | 1 << 4;


    public Light mountainLight, cityLight, floorLight;
    float mountainLightIntensity, cityLightIntensity;
    [HideInInspector]
    public enum Location
    {
        NOWHERE,
        MOUNTAINS,
        CITY,
        WEIRDROOM
    }
    [HideInInspector]
    public Location location;

    float sineWaveForYRot;

    [HideInInspector]
    public bool onFloor = true, descending, ascending, onSky;

    float upDownLook, lrLook;
    public float lookSpeed;

    public AudioSource overWorldSong, ambientAudio;
    public AudioClip mountainSound, citySound, wierdRoomSound;

    public bool onIOS;
    public float iosAdjustmentSpeed;

    bool clicked = false;// hasMovedTouch = false, hasStationaryTouch = false;


    //Vector3 oldMousePos, newMousePos;

    bool inSongArea;
    public bool inSong;
    public GameObject cloudSky;

    Color originalFogCol;
    float originalFogDens;

    public songManager songMan;
    string songAreaName;

    public Vector3 destinationPoint;
    public Transform muerteSubitaTrigger, futuroTrigger, mountainTrigger;

    public LayerMask songTriggerLayer;

    public headBob hb;

    bool inArea = false;


    public float slowDownSpeedLook;

    float yPosSpeedAdjustment;
    public Text mouseSensitivityDebug;

    // Use this for initialization
    void Start()
    {
        mountainLightIntensity = mountainLight.intensity;
        mountainLight.intensity = 0;
        cityLightIntensity = cityLight.intensity;
        cityLight.intensity = 0;


        ascending = true;

#if UNITY_IOS
        onIOS = true;
#endif

        normalCamHeightFloor = camHeightFloor;
        slowDownSpeedOriginal = slowDownSpeed;

        originalFogCol = RenderSettings.fogColor;
        originalFogDens = RenderSettings.fogDensity;

        destinationPoint = transform.position;

        inSong = true;

        location = Location.MOUNTAINS;

        mouseSensitivity = 1;

#if UNITY_WEBGL && !UNITY_EDITOR
        mouseSensitivity = 0.2f;
#endif


    }

    void Update()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (!onIOS)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                clicked = true;
            }
            else
                clicked = false;

            //HACER NOTA DE ESTO
            if (Input.GetKeyDown(KeyCode.Period))
                mouseSensitivity *= 1.25f;
            if (Input.GetKeyDown(KeyCode.Comma))
                mouseSensitivity /= 1.25f;

            mouseSensitivityDebug.text = "mouse sens: " + mouseSensitivity;

            //if (Input.GetKeyDown(KeyCode.M))
            //{
            //    if (mouseSensitivityDebug.gameObject.activeSelf)
            //        mouseSensitivityDebug.gameObject.SetActive(false);
            //    else
            //        mouseSensitivityDebug.gameObject.SetActive(true);

            //}

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
                //if (!inSong)
                //{
                ascending = true;
                onFloor = false;
                //}
                //else
                //{
                //    //poner un 'are you sure'
                //    songMan.stopSong();

                //}
            }
            clicked = false;
        }


        if (Input.GetKeyDown(KeyCode.M))
        {

            comoJugarMenu.transform.parent.gameObject.SetActive(true);
            comoJugarMenu.enabled = true;
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

            mouseX = Input.GetAxis("Mouse X") * Time.fixedDeltaTime * moveSpeed * mouseSensitivity;
            mouseY = Input.GetAxis("Mouse Y") * Time.fixedDeltaTime * moveSpeed * mouseSensitivity;
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


        if (inSongArea)
        {
            cityLight.enabled = false;
            inSong = true;
            if (!inSong)
            {
                //Text.color += new Color(0, 0, 0, Time.deltaTime * 0.2f);//Turn back on!!!

                //if (Text.color.a > 0.5f)//Turn back on!!!
                //{
                //ADJUST FOR IOS
                //if (Input.GetKeyDown(KeyCode.Space))
                //{
                //songMan.startSong(songAreaName);

                //}
                // }
            }
            else
            {


                //if (Input.GetKeyDown(KeyCode.Space))
                //{
                //    //poner un 'are you sure'
                //    songMan.stopSong();
                //    inSong = false;
                //}
            }

        }
        else
        {
            inSong = false;

            cityLight.enabled = true;
        }


        //if (!inSong)
        //{
        //    overWorldSong.volume = descendLerp;
        //    ambientAudio.volume = 1 - descendLerp;
        //}
        //else
        //{
        //    //if (ambientAudio.volume > 0)
        //    //    ambientAudio.volume -= Time.deltaTime;
        //    //overWorldSong.volume = 0;
        //}





    }



    void FlyMovement()
    {
        hb.enabled = false;
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
        Ray groundCheckRay = new Ray(transform.position + (Vector3.up * 100), Vector3.down);
        Physics.Raycast(groundCheckRay, out hit, 1000f, groundLayer);


        if (hit.collider != null)
        {
            groundHeight = hit.point.y;

            if (hit.collider.name == "nubeGround")
            {
                camHeightFloor = 80;
                cloudSky.SetActive(true);
            }
            else
            {
                camHeightFloor = normalCamHeightFloor;
                cloudSky.SetActive(false);
            }
        }
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

            //slowDownSpeed = 2f;//lerp this

        }
        //else
        //slowDownSpeed = slowDownSpeedOriginal;


        if (ascending)
        {

            descendLerp += Time.fixedDeltaTime * descendSpeed;

            descending = false;

        }



        descendLerp = Mathf.Clamp01(descendLerp);

        if (descendLerp > 0.99f && !descending)
            descendLerp = 1;

        if (speed.magnitude > 0.7f)//also change height!
        {
            //Camera.main.fov = Mathf.Lerp(Camera.main.fov, Camera.main.fov + Time.deltaTime * 50, 0.1f);
            //yPosSpeedAdjustment = Mathf.Lerp(yPosSpeedAdjustment, yPosSpeedAdjustment + Time.deltaTime * 70, 0.1f);
            //transform.position += (destination - transform.position) * speed * Time.deltaTime;
            Camera.main.fieldOfView += (110 - Camera.main.fieldOfView) * Time.deltaTime * 1.5f;
            yPosSpeedAdjustment += (60 - yPosSpeedAdjustment) * Time.deltaTime * 0.2f;


        }
        else
        {
            //Camera.main.fov = Mathf.Lerp(Camera.main.fov, Camera.main.fov - Time.deltaTime * 30, 0.1f);
            //yPosSpeedAdjustment = Mathf.Lerp(yPosSpeedAdjustment, yPosSpeedAdjustment - Time.deltaTime * 40, 0.1f);

            Camera.main.fieldOfView += (75 - Camera.main.fieldOfView) * Time.deltaTime * 0.3f;
            yPosSpeedAdjustment += (0 - yPosSpeedAdjustment) * Time.deltaTime * 0.1f;
        }
        //        Debug.Log(yPosSpeedAdjustment);

        yPosSpeedAdjustment = Mathf.Clamp(yPosSpeedAdjustment, 0, 60);
        //Camera.main.fov = Mathf.Lerp(75, Mathf.Lerp(75, Camera.main.fov * Mathf.Pow((speed.magnitude + 1), 2), 0.2f), 0.4f);
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 75, 110);

        //yPos = Mathf.Lerp(yPos, yPos * Mathf.Pow((speed.magnitude + 1), 6), 0.05f);



        skyLerp = Mathf.Sin(Time.time * 0.2f) * 0.5f + 0.5f;
        yPos = Mathf.Lerp(destinationPoint.y, Mathf.Lerp(camHeightDown, camHeightUp, skyLerp) + groundHeight + yPosSpeedAdjustment, descendLerp);



        slowDownSpeed = Mathf.Lerp(1f, slowDownSpeedOriginal, descendLerp);

        //        Debug.Log(descendLerp);

        if (ascending)
        {
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
            //yPos = Mathf.Lerp(destinationPoint.y, Mathf.Lerp(camHeightDown, camHeightUp, skyLerp) + groundHeight, descendLerp);

        }
        else
        {
            transform.position = Vector3.Lerp(destinationPoint, new Vector3(transform.position.x, yPos, transform.position.z), descendLerp);
            //yPos = Mathf.Lerp(camHeightDown, camHeightUp, skyLerp) + groundHeight;
        }


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

        sineWaveForYRot = Mathf.Sin(Time.time * 0.4f) * 0.7f;
        //        Debug.Log(sineWaveForYRot);

        transform.eulerAngles += Vector3.Lerp(Vector3.zero, new Vector3(0, (speed.x + speed.z) * sineWaveForYRot, 0), 0.7f);




        //------------------------------------------ WRAP AROUND MAP ------------------------------------------
        wrapAround();

        //------------------------------------------ LOCATION MANAGER ------------------------------------------


        switch (location)
        {

            case Location.NOWHERE:

                cityLight.intensity -= Time.fixedDeltaTime * 2 * cityLightIntensity;
                mountainLight.intensity -= Time.fixedDeltaTime * 2 * mountainLightIntensity;

                break;
            case Location.MOUNTAINS:


                if (Vector3.Distance(transform.position, destinationPoint) > 5)
                {
                    cityLight.intensity -= Time.fixedDeltaTime * 2 * cityLightIntensity;
                    mountainLight.intensity += Time.fixedDeltaTime * 0.2f * mountainLightIntensity;
                }
                else
                {
                    cityLight.intensity -= Time.fixedDeltaTime * 2 * cityLightIntensity;
                    mountainLight.intensity -= Time.fixedDeltaTime * 2f * mountainLightIntensity;
                }

                if (floorLight.intensity < 4)
                    floorLight.intensity += Time.fixedDeltaTime * 0.5f;



                //ambientAudio.clip = mountainSound;
                //if (!ambientAudio.isPlaying)
                //ambientAudio.Play();

                destinationPoint = mountainTrigger.position;

                clickPaBajar.text = "CLICK PARA BAJAR A LA MONTAÑA";

                break;
            case Location.CITY:

                cityLight.intensity += Time.fixedDeltaTime * 0.2f * cityLightIntensity;
                mountainLight.intensity -= Time.fixedDeltaTime * 2 * mountainLightIntensity;

                if (floorLight.intensity > 1)
                    floorLight.intensity -= Time.fixedDeltaTime * 1f;

                //ambientAudio.clip = citySound;
                //if (!ambientAudio.isPlaying)
                //ambientAudio.Play();

                destinationPoint = muerteSubitaTrigger.position;

                clickPaBajar.text = "CLICK PARA BAJAR A LA TERRAZA";

                break;

            case Location.WEIRDROOM:

                cityLight.intensity += Time.fixedDeltaTime * 0.05f * cityLightIntensity;
                mountainLight.intensity -= Time.fixedDeltaTime * 2 * mountainLightIntensity;

                if (floorLight.intensity > 1)
                    floorLight.intensity -= Time.fixedDeltaTime * 1f;

                //ambientAudio.clip = wierdRoomSound;
                //if (!ambientAudio.isPlaying)
                //ambientAudio.Play();

                destinationPoint = futuroTrigger.position;

                clickPaBajar.text = "CLICK PARA BAJAR A LA LAGUNA";

                break;
        }

        cityLight.intensity = Mathf.Clamp(cityLight.intensity, 0, cityLightIntensity);
        mountainLight.intensity = Mathf.Clamp(mountainLight.intensity, 0, mountainLightIntensity);

        if (descendLerp > 0.01)
            clickPaBajar.color = new Color(1, 1, 1, descendLerp);
        else
            clickPaBajar.color = new Color(1, 1, 1, 0);

    }

    void FPCamera()
    {
        //UPDOWN:


        upDownLook += mouseY * lookSpeed;
        //upDownLook = Mathf.Clamp(upDownLook, -80, 80);
        upDownLook = Mathf.Lerp(upDownLook, 0, slowDownSpeedLook);


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
        lrLook += mouseX * lookSpeed;
        lrLook = Mathf.Lerp(lrLook, 0, slowDownSpeedLook);

        transform.eulerAngles += new Vector3(transform.eulerAngles.x, lrLook, transform.eulerAngles.z);

        hb.enabled = true;
    }

    [Header("map boundaries: ")]
    public float minX = -150;
    public float maxX = 250;
    public float minZ = -150;
    public float maxZ = 530;

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


        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX),
                                         transform.position.y,
                                         Mathf.Clamp(transform.position.z, minZ, maxZ));


        if (!inArea)
        {
            location = Location.MOUNTAINS;
        }

        //Debug.Log(location);
    }



    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "songTrigger")
        {

            inSongArea = true;
            songAreaName = other.name;


            //if (other.gameObject.name == "muerte subita")
            //    muerteSubitaTrigger.GetComponent<MuerteSubitaManager>().enabled = true;

            //if (other.gameObject.name == "futuro")
            //futuroTrigger.GetComponent<FuturoManager>().enabled = true;


            if (other.tag == "brisas")
            {
                Camera.main.clearFlags = CameraClearFlags.Color;
                RenderSettings.fogColor = Color.white;
                RenderSettings.fogDensity = 0.01f;
            }

            if (location == Location.MOUNTAINS)
            {

            }

        }
        else
        {

            if (hit.collider != null && !descending)
            {
                if (other.tag == "mountains")
                    location = Location.MOUNTAINS;
                if (other.tag == "city")
                    location = Location.CITY;
                if (other.tag == "weirdRoom")
                    location = Location.WEIRDROOM;

            }
            else
            {
                location = Location.NOWHERE;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        inArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "songTrigger")
        {
            inSongArea = false;
            songAreaName = "";

            //muerteSubitaTrigger.GetComponent<MuerteSubitaManager>().enabled = false;
            //futuroTrigger.GetComponent<FuturoManager>().enabled = false;

            Camera.main.clearFlags = CameraClearFlags.Skybox;
            RenderSettings.fogColor = originalFogCol;
            RenderSettings.fogDensity = originalFogDens;
        }
        else
        {
            inArea = false;
        }
    }

}
