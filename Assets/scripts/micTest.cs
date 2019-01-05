using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class micTest : MonoBehaviour
{


    void Start()
    {
#if !UNITY_WEBGL
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = Microphone.Start(null, true, 1, 44100);
        audio.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) { }
        Debug.Log("start playing... position is " + Microphone.GetPosition(null));
        audio.Play();
#endif
    }
}