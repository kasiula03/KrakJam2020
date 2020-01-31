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
    [SerializeField] private string _targetKey = string.Empty;
    private StringReactiveProperty _watchProperty { get; set; }

    void Reset()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
    }

    void Start()
    {
        _watchProperty = _playerAbilities.GetProperty(_targetKey);
        Abilities.AddUnlockedAbility(_watchProperty.Value);
        _dropdown.options.Clear();
        int i = 0;
        int selected = 0;
        foreach (var ability in Abilities.UnlockedAbilities)
        {
            _dropdown.options.Add(new TMP_Dropdown.OptionData(ability));
            if (_watchProperty.Value == ability)
            {
                selected = i;
            }

            i++;
        }

        _dropdown.value = selected;
        
        _dropdown.onValueChanged.AddListener(OnSelectedValueChanged);
    }

    void OnSelectedValueChanged(int value)
    {
        _watchProperty.Value = _dropdown.options[value].text;
    }

    private void OnDestroy()
    { 
        _dropdown.onValueChanged.RemoveAllListeners();
    }
}
