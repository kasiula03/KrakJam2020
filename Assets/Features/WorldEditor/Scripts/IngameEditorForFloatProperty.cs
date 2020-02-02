using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Slider))]
public class IngameEditorForFloatProperty : MonoBehaviour
{
    [Inject] private WorldEditorLogic _worldLogic;
    
    public WorldEditorLogic.Properties _property;
    public float _min;
    public float _max;
    
    FloatReactiveProperty _watchProperty;

    [SerializeField] private Slider _slider;
    
    void Reset()
    {
        _slider = GetComponent<Slider>();
    }

    void Start()
    {
        _slider.minValue = _min;
        _slider.maxValue = _max;
        
        _watchProperty = _worldLogic.GetPropertyFor(_property);
        _slider.value = _watchProperty.Value;
        _slider.onValueChanged.AddListener(PropagateValueToProperty);

        if (_property == WorldEditorLogic.Properties.ColorBlue ||
            _property == WorldEditorLogic.Properties.ColorRed ||
            _property == WorldEditorLogic.Properties.ColorGreen)
        {
            WorldEditorLogic.ResetToWhite += PropagatePropertyToValue;
        }
    }

    void OnDisable()
    { 
        _slider.onValueChanged.RemoveListener(PropagateValueToProperty);
    }

    void PropagatePropertyToValue()
    {
        _slider.value = _watchProperty?.Value ?? 1f;
    }

    void PropagateValueToProperty(float f)
    {
        _watchProperty.Value = f;
    }

    private void OnDestroy()
    {
        WorldEditorLogic.ResetToWhite -= PropagatePropertyToValue;
    }
}
