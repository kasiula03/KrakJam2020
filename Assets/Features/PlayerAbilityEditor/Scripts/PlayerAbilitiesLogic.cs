using System;
using System.Collections.Generic;
using UniRx;
using Zenject;

public class PlayerAbilitiesLogic : IInitializable, IDisposable
{
    readonly Dictionary<string, StringReactiveProperty> OnEventPerformActionDictionary
        = new Dictionary<string, StringReactiveProperty>();

    readonly ReactiveCollection<string> UnlockedAbilities = new ReactiveCollection<string>();
    
    public void Initialize()
    {
        SetupBaseAbilities();
    }

    public void Dispose()
    {
        
    }

    public void UnlockAbility(string ability)
    {
        if (UnlockedAbilities.Contains(ability))
            return;

        UnlockedAbilities.Add(ability);
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
