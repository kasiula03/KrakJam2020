using UnityEngine;

public class LockMovingAction : SpecialAction
{
    [SerializeField] private SpriteRenderer _lockWheel;

    public override void Perform()
    {
        EnemyProperty properties = GetComponentInParent<EnemyProperty>();
        properties.isBlocked = true;
        _lockWheel.color = Color.white;
    }
}
