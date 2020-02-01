using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePalette : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _rednerer;
    void Update()
    {
        Color.RGBToHSV(_rednerer.color, out var h, out var s, out var v);
        h += Time.deltaTime;
        var c  = Color.HSVToRGB(h, s, v);
        _rednerer.color = new Color( c.r, c.g, c.b, .5f);
        
    }
}
