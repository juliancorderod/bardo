using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{

    public Transform player, futuroMan, muerSubMan;
    NewPlayer pScript;

    Image image;

    public RectTransform t, miniPlayer, miniWeirdRoom, miniCity;

    float scale = 0.005f;

    // Use this for initialization
    void Start()
    {

        pScript = player.GetComponent<NewPlayer>();

        image = GetComponent<Image>();

        miniWeirdRoom.localPosition = new Vector3(futuroMan.position.x, futuroMan.position.z, 0) * scale;

        miniCity.localPosition = new Vector3(muerSubMan.position.x, muerSubMan.position.z, 0) * scale;

    }

    // Update is called once per frame
    void Update()
    {

        if (!pScript.onSky)
        {
            if (image.color.a > 0)
                image.color -= new Color(0, 0, 0, Time.deltaTime);
        }
        else
        {
            if (image.color.a < 0.3f)
                image.color += new Color(0, 0, 0, Time.deltaTime);
        }


        t.localEulerAngles = new Vector3(0, 0, player.eulerAngles.y);
        miniPlayer.localPosition = new Vector3(player.position.x, player.position.z, 0) * scale;

    }
}
