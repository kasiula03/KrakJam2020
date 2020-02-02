using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DamageAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _damageColor;
   
    public Sequence Damage()
    {
        Sequence damageSequence = DOTween.Sequence();
        damageSequence.Append(_spriteRenderer.DOColor(_damageColor, 0.1f));
        damageSequence.Append(_spriteRenderer.DOColor(Color.white, 0.1f));
        return damageSequence;
    }
}
