﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComoJugarMenu : MonoBehaviour
{
    public GameObject[] slides;
    public GameObject siguiente, atras;
    public Image cover;
    public Text jugar;
    public NewPlayer p;

    int index;

    bool primeraVez = true, aJugar;

    // Use this for initialization
    void Start()
    {
        index = -1;
        siguienteClic();

    }

    // Update is called once per frame
    void Update()
    {


        if (index == 0)
            atras.SetActive(false);
        else
            atras.SetActive(true);

        if (index == 4)
            siguiente.SetActive(false);
        else
            siguiente.SetActive(true);

        index = Mathf.Clamp(index, 0, 4);


        if (aJugar)
        {
            if (cover.color.a < 1)
            {
                cover.color += new Color(0, 0, 0, Time.deltaTime * 0.5f);
            }
            else
            {
                transform.parent.gameObject.SetActive(false);
                this.enabled = false;
            }
        }

        if (!primeraVez)
            jugar.text = "VOLVER";
    }

    public void siguienteClic()
    {
        index++;
        for (int i = 0; i < slides.Length; i++)
            slides[i].SetActive(false);
        slides[index].SetActive(true);

    }
    public void atrasClic()
    {
        index--;
        for (int i = 0; i < slides.Length; i++)
            slides[i].SetActive(false);
        slides[index].SetActive(true);
    }

    public void jugarClic()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (primeraVez)
        {
            primeraVez = false;
            aJugar = true;
            cover.gameObject.SetActive(true);

        }
        else
        {
            p.enabled = true;
            transform.parent.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        index = -1;
        siguienteClic();

        cover.gameObject.SetActive(false);
        aJugar = false;
        cover.color = new Color(cover.color.r, cover.color.g, cover.color.b);
        p.enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

}