using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using Zenject;

public class PlayerAbilitiesLogic : IInitializable, IDisposable
{
    readonly Dictionary<Abilities.BindableReason, ReactiveProperty<Abilities.BindableReaction>> OnEventPerformActionDictionary
        = new Dictionary<Abilities.BindableReason, ReactiveProperty<Abilities.BindableReaction>>();

    readonly ReactiveCollection<Abilities.BindableReaction> unlockedAbilities 
        = new ReactiveCollection<Abilities.BindableReaction>();

    public IReadOnlyList<Abilities.BindableReaction> UnlockedAbilities
    {
        get => unlockedAbilities.ToList();
    }
    
    public void Initialize()
    {
        SetupBaseAbilities();
    }

    public void Dispose()
    {
        
    }

    public void UnlockAbility(Abilities.BindableReaction ability)
    {
        if (unlockedAbilities.Contains(ability))
            return;

        unlockedAbilities.Add(ability);
    }

    public ReactiveProperty<Abilities.BindableReaction> GetProperty(Abilities.BindableReason key)
    {
        return OnEventPerformActionDictionary[key];
    }

    void SetupBaseAbilities()
    {
        OnEventPerformActionDictionary.Clear();
        foreach (var key in (Abilities.BindableReason[])Enum.GetValues(typeof(Abilities.BindableReason)))
        {
            AddEmpty(key);
        }
        
        void AddEmpty(Abilities.BindableReason key)
        {
            OnEventPerformActionDictionary.Add(key, new ReactiveProperty<Abilities.BindableReaction>(Abilities.BindableReaction.Null));
        }
    }
}
