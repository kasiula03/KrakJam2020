using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEnemyCategory
{
    NoEnemy,
    Enemy1,
    Enemy2,
    Enemy3
}

public static class _ext_EEnemyCategory
{
    private const string E1 = "Enemy1";
    private const string E2 = "Enemy2";
    private const string E3 = "Enemy3";
    private const string NoEnemy = "NoEnemy";
    
    public static string GetName(this EEnemyCategory enemy)
    {
        switch (enemy)
        {
            case EEnemyCategory.Enemy1: return E1;
            case EEnemyCategory.Enemy2 : return E2;
            case EEnemyCategory.Enemy3: return E3;
            case EEnemyCategory.NoEnemy:
                default:
                return NoEnemy;
        }
    }

    public static EEnemyCategory ToEnemyCategory(this string enemyName)
    {
        switch (enemyName)
        {
            case E1 : return EEnemyCategory.Enemy1;
            case E2 : return EEnemyCategory.Enemy2;
            case E3 : return EEnemyCategory.Enemy3;
            case NoEnemy :
                default :
                return EEnemyCategory.NoEnemy;
        }
    }
}
