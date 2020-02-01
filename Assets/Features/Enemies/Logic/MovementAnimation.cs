using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class MovementAnimation : MonoBehaviour
{
    public bool isAnimating;
    public abstract Sequence Animate();
}
