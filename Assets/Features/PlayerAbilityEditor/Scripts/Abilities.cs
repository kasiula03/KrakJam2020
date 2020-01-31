using System.Collections.Generic;

public static class Abilities
{
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