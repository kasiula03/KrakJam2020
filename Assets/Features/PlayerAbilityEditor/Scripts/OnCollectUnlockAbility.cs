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
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            this.Collect();
        }
    }

    public void Collect()
    {
        _playerAbilities.UnlockAbility(ability);
        Destroy(this.gameObject);
    }
}
