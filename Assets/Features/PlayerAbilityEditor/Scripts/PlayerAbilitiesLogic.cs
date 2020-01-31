using System;
using System.Collections.Generic;
using UniRx;
using Zenject;

public class PlayerAbilitiesLogic : IInitializable, IDisposable
{
    private readonly Dictionary<string, StringReactiveProperty> OnEventPerformActionDictionary
        = new Dictionary<string, StringReactiveProperty>();

    public void Initialize()
    {
        SetupBaseAbilities();
    }

    public void Dispose()
    {
        
    }

    public StringReactiveProperty GetProperty(string key)
    {
        return OnEventPerformActionDictionary[key];
    }

    void SetupBaseAbilities()
    {
        OnEventPerformActionDictionary.Clear();
        foreach (var key in Abilities.OnEvents)
        {
            AddEmpty(key);
        }
        
        

        void AddEmpty(string key)
        {
            OnEventPerformActionDictionary.Add(key, new StringReactiveProperty(string.Empty));
        }
    }
}
