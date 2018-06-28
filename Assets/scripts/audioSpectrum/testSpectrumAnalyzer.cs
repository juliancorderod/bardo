using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class testSpectrumAnalyzer : MonoBehaviour
{



    void Update()
    {
        float[] spectrum = new float[256];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
            Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 0), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 0), Color.cyan);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 0), new Vector3(Mathf.Log(i), spectrum[i] - 10, 0), Color.green);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 0), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 0), Color.blue);

            //Debug.Log(spectrum[i]);
        }


    }
}