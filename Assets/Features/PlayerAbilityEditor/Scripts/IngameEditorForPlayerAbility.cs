using System;
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
        _playerAbilities.UnlockAbility(Abilities.BindableReaction.Fire);
        _playerAbilities.UnlockAbility(Abilities.BindableReaction.Jump);
        
        _watchProperty = _playerAbilities.GetProperty(_targetKey);
        _playerAbilities.UnlockAbility(_watchProperty.Value);
        _dropdown.ClearOptions();
        int i = 0;
        int selected = 0;
        foreach (var ability in _playerAbilities.UnlockedAbilities)
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
        _dropdown.onValueChanged.RemoveAllListeners();
    }
}
