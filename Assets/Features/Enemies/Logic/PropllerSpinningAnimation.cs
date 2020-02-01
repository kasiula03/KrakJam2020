using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropllerSpinningAnimation : MovementAnimation
{
    [SerializeField] private Transform _circleLeft;
    [SerializeField] private Transform _circleRight;
    [SerializeField] private float _duration;

    public override Sequence Animate()
    {
        Sequence animation = DOTween.Sequence();
        animation.Insert(0, _circleLeft.DOLocalRotate(new Vector3(0, 180f, 0), _duration));
        animation.Insert(0, _circleRight.DOLocalRotate(new Vector3(0, 180f, 0), _duration));
        animation.SetLoops(-1, LoopType.Incremental);
        return animation;
    }
}
