using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class songManager : MonoBehaviour
{

    AudioSource a;

    public AudioClip gato, brisas, victoria, muerteSubita, masai, futuro, agua;

    public NewPlayer playerScript;

    public AudioClip[] songs;
    int songInt;
    float songTimer;

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
    }

    public void startSong(string songName)
    {
        AudioClip songToPlay = null;

        if (songName == "gato")
            songToPlay = gato;
        else if (songName == "brisas")
            songToPlay = brisas;
        else if (songName == "victoria")
            songToPlay = victoria;
        else if (songName == "muerte subita")
            songToPlay = muerteSubita;
        else if (songName == "masai")
            songToPlay = masai;
        else if (songName == "futuro")
            songToPlay = futuro;
        else if (songName == "agua")
            songToPlay = agua;
        else
            Debug.Log("wrong songName");



        a.clip = songToPlay;

        a.PlayDelayed(2f);

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
