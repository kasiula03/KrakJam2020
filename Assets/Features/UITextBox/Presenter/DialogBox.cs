using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class DialogBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _speach;
    [SerializeField] private TextMeshProUGUI _speakerName;

    public void Initialize()
    {
        transform.localScale = Vector3.zero;
    }

    public void Show(string name, string text)
    {
        _speakerName.text = name;
        _speach.text = text;
        Sequence displayText = DOTween.Sequence();
        displayText.Append(transform.DOScale(Vector3.one, 1f).SetEase(Ease.InCirc));
    }

    public void Hide(Action postHide)
    {
        Sequence displayText = DOTween.Sequence();
        displayText.Append(transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InCirc));
        displayText.onComplete += () => postHide.Invoke();
    }
}
