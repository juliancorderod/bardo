using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class songManager : MonoBehaviour
{

    AudioSource a;

    public NewPlayer playerScript;

    public AudioClip[] songs;
    int songInt;
    float songTimer;

    public GameObject DebugVisualizer;

    // Use this for initialization
    void Start()
    {
        a = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        //if (playerScript.inSong)
        //{
        //    if (!a.isPlaying)
        //        stopSong();
        //}


        songTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (DebugVisualizer.activeSelf)
                DebugVisualizer.SetActive(false);
            else
                DebugVisualizer.SetActive(true);
        }
    }


    public void NextSong()
    {
        songInt++;
        if (songInt == songs.Length)
            songInt = 0;

        a.clip = songs[songInt];

        a.Play();
        songTimer = 0;
    }

    public void PreviousSong()
    {
        if (songTimer < 3)
        {
            songInt--;
            if (songInt == -1)
                songInt = songs.Length - 1;
        }
        a.clip = songs[songInt];


        a.Play();
        songTimer = 0;
    }

    public void stopSong()
    {
        a.Stop();
        a.clip = null;
        playerScript.inSong = false;
        //slowly reset everything here
        songTimer = 0;
    }
}
