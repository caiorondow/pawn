using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CustomPostProcess : MonoBehaviour
{
    public Material mat;

    // [ImageEffectOpaque]
    void OnRenderImage(RenderTexture src, RenderTexture dest) 
    {
        Graphics.Blit(src, dest, mat);    
    }
}
