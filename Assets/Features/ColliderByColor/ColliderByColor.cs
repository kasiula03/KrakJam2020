using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider2D))]
public class ColliderByColor : MonoBehaviour
{
    public enum EColor
    {
        None,
        Red,
        Green,
        Blue
    }
    
    [SerializeField] private Collider2D _collider;
    [Inject] private WorldEditorLogic _world;
    [SerializeField] private EColor colliderColor;
    void Reset()
    {
        _collider = GetComponent<Collider2D>();
    }

    void Start()
    {
        _world.ColorResult.Subscribe(OnColorChanged).AddTo(this);
        
    }

    void OnColorChanged(Color c)
    {
        if (this.colliderColor == EColor.Red && Mathf.Approximately(c.r, 0))
        {
            _collider.enabled = false;
            return;
        }
        
        if (this.colliderColor == EColor.Green && Mathf.Approximately(c.g, 0))
        {
            _collider.enabled = false;
            return;
        }
        
        if (this.colliderColor == EColor.Blue && Mathf.Approximately(c.b, 0))
        {
            _collider.enabled = false;
            return;
        }

        _collider.enabled = true;
    }
}
