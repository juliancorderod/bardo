using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class PostProcessEffect : MonoBehaviour
{

    public Material Mat;
    public int pass;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //CommandBuffer cb = new CommandBuffer();
        Graphics.Blit(source, destination, Mat, pass);

    }
}
