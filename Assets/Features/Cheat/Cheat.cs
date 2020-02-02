using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Cheat : MonoBehaviour
{
    [Inject] private PlayerAbilitiesLogic _playerAbilities; 
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            _playerAbilities.GetProperty(Abilities.BindableReason.LeftMovement).Value =
                Abilities.BindableReaction.MoveLeft;
            _playerAbilities.GetProperty(Abilities.BindableReason.RightMovement).Value =
                Abilities.BindableReaction.MoveRight;
            _playerAbilities.GetProperty(Abilities.BindableReason.FireButtonPressed).Value =
                Abilities.BindableReaction.Fire;
            _playerAbilities.GetProperty(Abilities.BindableReason.JumpButtonPressed).Value =
                Abilities.BindableReaction.Jump;
        }
    }
}
