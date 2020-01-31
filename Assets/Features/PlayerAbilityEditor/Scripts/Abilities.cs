using System;
using System.Collections.Generic;

public class Abilities
{
    public enum Bindable
    {
        Jump,
        Fire,
        VomitAnimation,
    }

    public static Dictionary<Bindable, Action<PlayerController>> BindedActions =
        new Dictionary<Bindable, Action<PlayerController>>()
        {    
            { Bindable.Jump, ExecuteJump },
            { Bindable.Fire, ExecuteFire }
        };

    public static void ExecuteFire(PlayerController player)
    {
        player.Fire();
    }
    
    public static void ExecuteJump(PlayerController player)
    {
        player.Jump();
    }
    
    public static string[] OnEvents = 
    {
        "OnPressJumpButton",
        "OnPressFireButton",
        "OnEnterFire",
        "OnReactWithWater",
        "OnReactWithElectric"
    };

    public static string[] SingleButtonActions =
    {
        "Jump",
        "ShootLaser",
        "Vomit",
        "Die",
        "Duck",
        "WaterGun",
        "null",
        "Roll"
    };

    public static string[] OneParameterActions =
    {
        "UnlockPower"
    };

    public static List<string> UnlockedAbilities = new List<string>();

    public static void AddUnlockedAbility(string ability)
    {
        if (UnlockedAbilities.Contains(ability))
            return;
        
        UnlockedAbilities.Add(ability);
    }
}