using DG.Tweening;
using UnityEngine;

public class CodeCanvas : MonoBehaviour
{
    private const float Time = 1f;

    [ContextMenuItem(nameof(Show), nameof(Show))]
    [ContextMenuItem(nameof(Hide), nameof(Hide))]
    public string Cheats = "";

    public bool IsShown { get; private set; } = false;

    void Awake()
    {
        transform.localScale = Vector3.zero;
    }
    
    public Tween Show()
    {
        if (IsShown)
            return DOTween.Sequence().Play();

        IsShown = true;
        return DOTween.Sequence().Append(transform.DOScale(Vector3.one, Time).SetEase(Ease.InCirc)).Play();
    }

    public Tween Hide()
    {
        if (IsShown == false)
            return DOTween.Sequence().Play();

        IsShown = false;
        return DOTween.Sequence().Append(transform.DOScale(Vector3.zero, Time).SetEase(Ease.InCirc)).Play();
    }
}
