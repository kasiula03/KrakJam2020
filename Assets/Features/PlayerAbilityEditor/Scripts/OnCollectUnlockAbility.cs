using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

public class OnCollectUnlockAbility : MonoBehaviour
{
    [Inject] private PlayerAbilitiesLogic _playerAbilities;
    [SerializeField] private string _abilityName = string.Empty;

    void Start()
    {
        Assert.IsFalse(string.IsNullOrEmpty(_abilityName));
    }
    
    // TODO - call when player collects this item
    public void OnCollect()
    {
        _playerAbilities.UnlockAbility(_abilityName);
    }
}
