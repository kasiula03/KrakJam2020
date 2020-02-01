using System;
using UniRx;
using UnityEngine;
using Zenject;

public class WorldEditorLogic : IDisposable, IInitializable
{
    public enum Properties
    {
        ColorRed,
        ColorGreen,
        ColorBlue,
        Gravity
    }

    public ReadOnlyReactiveProperty<Color> ColorResult { get; private set; }
    public ReadOnlyReactiveProperty<float> GravityResult { get; private set; }
    
    

    readonly FloatReactiveProperty Gravity = new FloatReactiveProperty(9f);
    readonly FloatReactiveProperty MinGravity = new FloatReactiveProperty(-9f);
    readonly FloatReactiveProperty MaxGravity = new FloatReactiveProperty(12f);

    readonly FloatReactiveProperty ColorRed = new FloatReactiveProperty(1);
    readonly FloatReactiveProperty ColorGreen = new FloatReactiveProperty(1);
    readonly FloatReactiveProperty ColorBlue = new FloatReactiveProperty(1);

    readonly FloatReactiveProperty NULLProperty = new FloatReactiveProperty(0f);
    
    readonly CompositeDisposable _disposable = new CompositeDisposable();

    public FloatReactiveProperty GetPropertyFor(Properties p)
    {
        switch (p)
        {
            case Properties.ColorRed : return ColorRed;
            case Properties.ColorBlue : return ColorBlue;
            case Properties.ColorGreen : return ColorGreen;
            case Properties.Gravity : return Gravity;
            default: return NULLProperty;
        }
    }

    private Color CalculateColor(float red, float green, float blue)
    {
        return new Color(red, green, blue, 1);
    }

    public float CalculateGravity(float current, float min, float max)
    {
        return Mathf.Clamp(current, min, max);
    }

    public void Dispose()
    {
        _disposable.Dispose();
    }

    public void Initialize()
    {
        ColorResult = ColorRed
            .CombineLatest(ColorGreen, ColorBlue, CalculateColor)
            .ToReadOnlyReactiveProperty()
            .AddTo(_disposable);

        GravityResult = Gravity
            .CombineLatest(MinGravity, MaxGravity, CalculateGravity)
            .ToReadOnlyReactiveProperty()
            .AddTo(_disposable);

        ColorResult.Subscribe(SetColorFilter).AddTo(_disposable);
        Gravity.Subscribe(SetGravity2D).AddTo(_disposable);
    }

    void SetColorFilter(Color c)
    {
        CameraPostProcess.Instance.material.color = c;
    }

    void SetGravity2D(float gravity)
    {
        Physics2D.gravity = Vector2.down * gravity;
    }
}
