using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class Fader : MonoBehaviour
{
    public bool start = false;
    public float fadeDamp = 0f;
    public string fadeScene;
    public float alpha = 0f;
    public Color fadeColor;
    public bool isFadin;
    CanvasGroup myCanvas;
    Image bg;
}
