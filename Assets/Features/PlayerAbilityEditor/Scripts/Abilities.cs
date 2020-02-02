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
        LockWheelButtonPressed,
        LaserButtonPressed,
        LeftMovement,
        RightMovement,

    }
    
    public enum BindableReaction
    {
        Jump,
        Fire,
        Vomit,
        Jetpack,
        Laser,

        MoveLeft,
        MoveRight,
        Lock,
        Null,
        SelectRGB
    }
    

    public static Dictionary<BindableReaction, Action<PlayerController>> BindedActions =
        new Dictionary<BindableReaction, Action<PlayerController>>()
        {    
            { BindableReaction.Jump, ExecuteJump },
            { BindableReaction.Fire, ExecuteFire },
            { BindableReaction.Jetpack, ExecuteJetpack },
            { BindableReaction.Lock, ExecuteLock },
            { BindableReaction.Null, ExecuteDance },
            { BindableReaction.Laser, ExecuteLaser },
            { BindableReaction.Vomit, _ => { }},
            { BindableReaction.MoveLeft, ExecuteMoveLeft},
            { BindableReaction.MoveRight, ExecuteMoveRight},
            { BindableReaction.SelectRGB, ExecuteSelectRGB }
        };

    public static void ExecuteSelectRGB(PlayerController player)
    {
        player.ShowHideSelectRGB();
    }

    public static void ExecuteLock(PlayerController player)
    {
        player.PerformSpecialAction();
    }

    public static void ExecuteDance(PlayerController player)
    {
        player.Dance();
    }

    public static void ExecuteFire(PlayerController player)
    {
        player.Fire();
    }
    public static void ExecuteLaser(PlayerController player)
    {
        player.Laser();
    }


    public static void ExecuteMoveLeft(PlayerController player)
    {
        player.MoveLeft();
    }

    public static void ExecuteMoveRight(PlayerController player)
    {
        player.MoveRight();
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