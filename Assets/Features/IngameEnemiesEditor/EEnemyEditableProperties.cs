using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EEnemyEditableProperties
{
    public enum Targets
    {
        Movement,
        Shooting,
        
    }
    
    public enum VerticalVector
    {
        None,
        Left,
        Right,
        AsPlayer,
        OppositeToPlayer,
        Sine, // ?
        DevUrandom // ?
    }
}
