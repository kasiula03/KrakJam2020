using System;
using System.Collections.Generic;

public class Abilities
{
    public enum BindableReason
    {
        JumpButtonPressed,
        FireButtonPressed,
    }
    
    public enum BindableReaction
    {
        Jump,
        Fire,
        Vomit,
        Null
    }

    public static Dictionary<BindableReaction, Action<PlayerController>> BindedActions =
        new Dictionary<BindableReaction, Action<PlayerController>>()
        {    
            { BindableReaction.Jump, ExecuteJump },
            { BindableReaction.Fire, ExecuteFire },
            { BindableReaction.Null, _ => { }},
            { BindableReaction.Vomit, _ => { }}
        };

    public static void ExecuteFire(PlayerController player)
    {
        player.Fire();
    }
    
    public static void ExecuteJump(PlayerController player)
    {
        player.Jump();
    }
    
    public static List<string> UnlockedAbilities = new List<string>();

}