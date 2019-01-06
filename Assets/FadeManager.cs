using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public GameObject menu, juegoCanvas;
    public Image cover;
    public Text title;

    public NewPlayer p;
    public GameObject song;

    bool activateGame, activateTitle;
    float titleTimer;

    // Use this for initialization
    void Start()
    {
        title.gameObject.SetActive(false);
        juegoCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!activateGame)
        {
            if (!menu.activeSelf)
            {
                if (cover.color.a > 0)
                {
                    cover.color -= new Color(0, 0, 0, Time.deltaTime * 0.25f);

                }
                else
                {
                    activateTitle = true;
                }

            }

            if (activateTitle)
            {

                song.GetComponent<songManager>().enabled = true;
                song.GetComponent<AudioSource>().enabled = true;

                title.gameObject.SetActive(true);

                if (titleTimer < 5)
                    titleTimer += Time.deltaTime;
                else
                {
                    title.color -= new Color(0, 0, 0, Time.deltaTime * 0.5f);

                    if (title.color.a < 0)
                    {
                        title.gameObject.SetActive(false);
                        activateGame = true;
                    }
                }
            }
        }
        else
        {
            p.enabled = true;
            juegoCanvas.SetActive(true);
            this.enabled = false;
        }
    }


}
