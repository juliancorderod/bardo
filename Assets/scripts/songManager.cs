using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class songManager : MonoBehaviour
{

    AudioSource a;

    public AudioClip gato, brisas, victoria, muerteSubita, masai, futuro, agua;

    public NewPlayer playerScript;



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

    public void stopSong()
    {
        a.Stop();
        a.clip = null;
        playerScript.inSong = false;
        //slowly reset everything here

    }
}
