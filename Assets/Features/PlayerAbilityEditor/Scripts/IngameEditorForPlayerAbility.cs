﻿using System;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TMP_Dropdown))]
public class IngameEditorForPlayerAbility : MonoBehaviour
{
    [Inject] private PlayerAbilitiesLogic _playerAbilities;
    
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private Abilities.BindableReason _targetKey;
    private ReactiveProperty<Abilities.BindableReaction> _watchProperty { get; set; }

    void Reset()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
    }

    void Start()
    {
        //_playerAbilities.UnlockAbility(Abilities.BindableReaction.Fire);
        //_playerAbilities.UnlockAbility(Abilities.BindableReaction.Jump);

        _playerAbilities.UnlockedAbilitiesChanged += this.RefreshAbilities;
        _watchProperty = _playerAbilities.GetProperty(_targetKey);
        _playerAbilities.UnlockAbility(_watchProperty.Value);
        RefreshAbilities();
        
    }

    private static List<Abilities.BindableReaction> MovementFilter = new List<Abilities.BindableReaction>()
    {
        Abilities.BindableReaction.MoveLeft,
        Abilities.BindableReaction.MoveRight
    };

    void RefreshAbilities()
    {
        _dropdown.ClearOptions();
        var abilitiesList = _playerAbilities.UnlockedAbilities;
        if (_targetKey == Abilities.BindableReason.LeftMovement || _targetKey == Abilities.BindableReason.RightMovement)
        {
            abilitiesList = MovementFilter;
        }
        int i = 0;
        int selected = 0;
        foreach (var ability in abilitiesList)
        {
            _dropdown.options.Add(new TMP_Dropdown.OptionData(ability.ToString()));
            if (_watchProperty.Value == ability)
            {
                selected = i;
            }

            i++;
        }

        _dropdown.value = selected;
        _dropdown.RefreshShownValue();
        
        _dropdown.onValueChanged.AddListener(OnSelectedValueChanged);
    }

    void OnSelectedValueChanged(int value)
    {
        _watchProperty.Value = (Abilities.BindableReaction)
            Enum.Parse(typeof(Abilities.BindableReaction), _dropdown.options[value].text);
    }

    private void OnDestroy()
    {
        _playerAbilities.UnlockedAbilitiesChanged -= this.RefreshAbilities;
        _dropdown.onValueChanged.RemoveAllListeners();
    }
}
