using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTrigger : TriggerPoint
{

    public override void OnTriggerAction(Collision2D collider)
    {
        Destroy(gameObject);
    }
}
