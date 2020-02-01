using UnityEngine;
using DG.Tweening;


public class CarWheelAnimation : MovementAnimation
{
    [SerializeField] private Transform _wheelFront;
    [SerializeField] private Transform _wheelBack;
    [SerializeField] private float _duration;

    public override Sequence Animate()
    {
        Sequence animation = DOTween.Sequence();
        animation.Insert(0, _wheelFront.DOLocalRotate(new Vector3(0, 0, 180f), _duration));
        animation.Insert(0, _wheelBack.DOLocalRotate(new Vector3(0, 0, 180f), _duration));
        animation.SetLoops(-1, LoopType.Incremental);
        return animation;
    }
}
