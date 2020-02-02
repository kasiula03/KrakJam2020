using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInput : MonoBehaviour
{
    public Color colorWhenInactive = new Color(1f, 1f, 1f, .7f);
    public Color colorWhenActive = new Color(1f, 1f, 1f, 1f);
    public Graphic output;
    public KeyCode listenFor;

    void Update()
    {
        if (Input.GetKey(listenFor))
        {
            output.color = colorWhenActive;
        }
        else
        {
            output.color = colorWhenInactive;
        }
    }
}
