using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{

    public Transform player, futuroMan, muerSubMan;
    NewPlayer pScript;

    Image mainImage, pImage, weirdRoomImage, cityImage;

    public RectTransform t, miniPlayer, miniWeirdRoom, miniCity;

    float tim;

    NewPlayer.Location previousLocation;
    bool changedLocation;

    public Image arrowsImage;

    // Use this for initialization
    void Start()
    {

        pScript = player.GetComponent<NewPlayer>();

        mainImage = GetComponent<Image>();
        pImage = miniPlayer.GetComponent<Image>();
        weirdRoomImage = miniWeirdRoom.GetComponent<Image>();
        cityImage = miniCity.GetComponent<Image>();

        miniWeirdRoom.localPosition = new Vector3(Geo.remapRange(futuroMan.position.x, pScript.minX, pScript.maxX, -45, 45),
                                                  Geo.remapRange(futuroMan.position.z, pScript.minZ, pScript.maxZ, -45, 45), 0);
        miniCity.localPosition = new Vector3(Geo.remapRange(muerSubMan.position.x, pScript.minX, pScript.maxX, -45, 45),
                                                  Geo.remapRange(muerSubMan.position.z, pScript.minZ, pScript.maxZ, -45, 45), 0);
        //miniWeirdRoom.localPosition = new Vector3(futuroMan.position.x, futuroMan.position.z - 100, 0) * scale;
        //miniCity.localPosition = new Vector3(muerSubMan.position.x, muerSubMan.position.z - 100, 0) * scale;

    }

    // Update is called once per frame
    void Update()
    {

        if (previousLocation != pScript.location)
        {
            changedLocation = true;
        }
        //else
        //changedLocation = false;

        //        Debug.Log(changedLocation);

        previousLocation = pScript.location;



        //if (!pScript.onSky)
        //{
        //    DisappearMap(true);
        //}
        //else
        //{
        //    //not sure if i like it to appear when you change locations
        //    //if (changedLocation)
        //    //{
        //    //    AppearMap();
        //    //}


        //    if (Input.GetAxis("Mouse X") == 0 || Input.GetAxis("Mouse Y") == 0)
        //    {
        //        if (tim < 5)
        //        {
        //            tim += Time.deltaTime;
        //            //Debug.Log(tim);
        //        }
        //        else
        //        {
        //            AppearMap(true);
        //        }
        //    }
        //    else
        //    {
        //        tim = 0;
        //        DisappearMap(true);
        //    }
        //}

        AppearMap(true);
        t.localEulerAngles = new Vector3(0, 0, player.eulerAngles.y);
        miniPlayer.localPosition = new Vector3(Geo.remapRange(player.position.x, pScript.minX, pScript.maxX, -45, 45),
                                                  Geo.remapRange(player.position.z, pScript.minZ, pScript.maxZ, -45, 45), 0);
        //miniPlayer.localPosition = new Vector3(player.position.x, player.position.z - 100, 0) * scale;



        switch (pScript.location)
        {
            case NewPlayer.Location.MOUNTAINS:

                pImage.color = Color.Lerp(pImage.color, new Color(0, 1, 0, pImage.color.a), 0.025f);

                break;
            case NewPlayer.Location.CITY:

                pImage.color = Color.Lerp(pImage.color, new Color(cityImage.color.r, cityImage.color.g, cityImage.color.b, pImage.color.a), 0.025f);

                break;
            case NewPlayer.Location.WEIRDROOM:

                pImage.color = Color.Lerp(pImage.color, new Color(weirdRoomImage.color.r, weirdRoomImage.color.g, weirdRoomImage.color.b, pImage.color.a), 0.025f);

                break;

        }




    }

    void DisappearMap(bool fast)
    {
        float speed = 0;
        if (fast)
            speed = 1;
        else
            speed = 0.25f;

        if (mainImage.color.a > 0)
        {
            mainImage.color -= new Color(0, 0, 0, Time.deltaTime * speed);
            pImage.color -= new Color(0, 0, 0, Time.deltaTime * speed);
            weirdRoomImage.color -= new Color(0, 0, 0, Time.deltaTime * speed);
            cityImage.color -= new Color(0, 0, 0, Time.deltaTime * speed);
            arrowsImage.color -= new Color(0, 0, 0, Time.deltaTime * speed);
        }
    }
    void AppearMap(bool withArrows)
    {
        if (mainImage.color.a < 0.7f)
        {
            mainImage.color += new Color(0, 0, 0, Time.deltaTime);
            pImage.color += new Color(0, 0, 0, Time.deltaTime);
            weirdRoomImage.color += new Color(0, 0, 0, Time.deltaTime);
            cityImage.color += new Color(0, 0, 0, Time.deltaTime);

            if (withArrows)
                arrowsImage.color += new Color(0, 0, 0, Time.deltaTime);//NOT SURE HOW I FEEL ABOUT THE ARROWS
        }
        else
        {
            changedLocation = false;
        }
    }
}
