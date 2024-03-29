﻿using UnityEngine;
using System.Collections;

public class MainFader : MonoBehaviour {

    public Texture2D fadeOutTexture;
    public float fadeSpeed;

    private int drawDepth = -1000;
    public float alpha = 1.0f;

    private int fadeDir = -1;
    // Use this for initialization
    void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }

    public float BeginFade(int dir)
    {
        fadeDir = dir;
        return (fadeSpeed);
    }

    void OnEnabled()
    {
        // alpha = 1;
        BeginFade(-1);
    }
}
