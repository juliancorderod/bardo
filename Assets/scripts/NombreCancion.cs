using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class NombreCancion : MonoBehaviour
{


    public AudioSource songSource;
    Text t;
    public string mueSub;

    // Use this for initialization
    void Start()
    {
        t = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if (songSource.clip != null)
            t.text = songSource.clip.name.ToUpper();
    }
}
