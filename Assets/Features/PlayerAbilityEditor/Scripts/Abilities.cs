using System;
using System.Collections.Generic;
using UnityEngine;

public class Abilities
{
    public enum BindableReason
    {
        JumpButtonPressed,
        FireButtonPressed,
        JetpackButtonPressed,
    }
    
    public enum BindableReaction
    {
        Jump,
        Fire,
        Vomit,
        Jetpack,
        Null
    }

    public static Dictionary<BindableReaction, Action<PlayerController>> BindedActions =
        new Dictionary<BindableReaction, Action<PlayerController>>()
        {    
            { BindableReaction.Jump, ExecuteJump },
            { BindableReaction.Fire, ExecuteFire },
            { BindableReaction.Jetpack, ExecuteJetpack },
            { BindableReaction.Null, _ => { }},
            { BindableReaction.Vomit, _ => { }}
        };

    public static void ExecuteFire(PlayerController player)
    {
        player.Fire();
    }

    //public static bool isJetPack = false;
    public static void ExecuteJetpack(PlayerController player)
    {
        var usedButton = player.currentKeyPressed;
        player.StartFly(usedButton);
    }
    public static void ExecuteJump(PlayerController player)
    {
        player.Jump();
    }
    
    public static List<string> UnlockedAbilities = new List<string>();

}