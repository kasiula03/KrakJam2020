using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerPoint : MonoBehaviour
{
    [SerializeField]
    private string _targetTag;

    public abstract void OnTriggerAction(Collision2D collider);

    public void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.tag == _targetTag) {
            OnTriggerAction(collider);
        }
    }
}
