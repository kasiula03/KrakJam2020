using UnityEngine;

public class EnemyKillingCondition : MonoBehaviour
{
    private EnemyProperty _properties;

    public bool targetInRange;
    public bool blocked;
    public bool facingRight;
    public bool blind;

    private void Start()
    {
        _properties = GetComponent<EnemyProperty>();
    }

    public bool IsKillable()
    {
        if (!targetInRange && !blocked && ! facingRight && !blind)
        {
            return true;
        }

        return _properties.targetInRange == targetInRange && _properties.facingRight == facingRight && _properties.isBlocked == blocked;
    }
}
