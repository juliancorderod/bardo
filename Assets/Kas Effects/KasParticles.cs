using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KasParticles : MonoBehaviour {
    AudioSpectrum spectrum;
    FuturoManager fm;
    public ParticleSystem[] crystals;
    public ParticleSystem[] horCrystals;
    public ParticleSystem[] otherEffects;

    public float crystalsMin;
    public float horCrystalsMin;

    bool activated;

    void Start () {
        spectrum = GameObject.FindGameObjectWithTag("songMan").GetComponent<AudioSpectrum>();
        fm = GetComponent<FuturoManager>();
        //this.enabled = false;
        for(int i = 0; i < crystals.Length; i++)
        {
            crystals[i].Stop();
        }
        for (int i = 0; i < horCrystals.Length; i++)
        {
            horCrystals[i].Stop();
        }
        for(int i = 0; i < otherEffects.Length; i++)
        {
            otherEffects[i].Stop();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (fm.enabled)
        {
            Debug.Log(spectrum.MeanLevels[8] * 100);
            //this.enabled = true;
            if(spectrum.MeanLevels[8] * 100 > crystalsMin)
            {
                for (int i = 0; i < crystals.Length; i++)
                {
                    crystals[i].Play();
                }
            }

            Debug.Log(spectrum.MeanLevels[7] * 100);
            //this.enabled = true;
            if (spectrum.MeanLevels[7] * 100 > horCrystalsMin)
            {
                for (int i = 0; i < horCrystals.Length; i++)
                {
                    horCrystals[i].Play();
                }
            }

            if (!activated)
            {
                for (int i = 0; i < otherEffects.Length; i++)
                {
                    otherEffects[i].Play();
                   
                }
                activated = true;
            }
           
        }
        else
        {
            //this.enabled = false;
            for (int i = 0; i < crystals.Length; i++)
            {
                crystals[i].Stop();
            }
            for (int i = 0; i < horCrystals.Length; i++)
            {
                horCrystals[i].Stop();
            }
            if (activated)
            {
                for (int i = 0; i < otherEffects.Length; i++)
                {
                    otherEffects[i].Stop();
                }
                activated = false;
            }
           
        }


	}
}
