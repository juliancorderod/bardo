using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CamViewport : MonoBehaviour
{
    public RectTransform vignette;
    CanvasScaler cs;
    Canvas c;

    public Texture2D vignetteTexture;


    // Use this for initialization
    void Start()
    {
        //cs = GetComponent<CanvasScaler>();
        c = GetComponent<Canvas>();
        //vignette.sizeDelta = new Vector2(cs.referenceResolution.x * Camera.main.rect.width, cs.referenceResolution.y * Camera.main.rect.height);
        //vignette.anchoredPosition = new Vector2(0, (cs.referenceResolution.y * Camera.main.rect.x) / 2);// + cs.referenceResolution.y / 2);

        c.pixelRect.Set(0.1f * Screen.width, 0.15f * Screen.height, 0.8f * Screen.width, 0.8f * Screen.height);
    }

    void OnGUI()
    {
        //GUI.DrawTexture(new Rect(0.1f * Screen.width, 0.15f * Screen.height, 0.8f * Screen.width, 0.8f * Screen.height), vignetteTexture);

    }

    // Update is called once per frame
    void Update()
    {
        //Camera.main.rect = new Rect(0.2f, 0.2f, 0.6f, 0.6f);
        //Rect r = new Rect(0f, 0f, 0.85f, 0.85f);

        //float w = r.width * Screen.width, h = r.height * Screen.height;
        //if (w > h)
        //{
        //    r.width = h / Screen.width;
        //}
        //else
        //{
        //    r.height = w / Screen.height;
        //}
        //r.center = new Vector2(0.5f, 0.57f);

        //Camera.main.rect = r;



    }
}
