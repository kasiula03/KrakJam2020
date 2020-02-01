using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

public class OnCollectUnlockAbility : MonoBehaviour
{
    [Inject] private PlayerAbilitiesLogic _playerAbilities;
    [SerializeField] private Abilities.BindableReaction ability = Abilities.BindableReaction.Null;

    void Start()
    {
        Assert.IsFalse(ability == Abilities.BindableReaction.Null);
    }
    
    // TODO - call when player collects this item
    public void OnCollect()
    {
        _playerAbilities.UnlockAbility(ability);
    }
}
