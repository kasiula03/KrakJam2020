using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionOnPlayer : MonoBehaviour
{
    [SerializeField] protected bool _isOneTimeAction;

    protected bool executed;

    public abstract void DoAction(Vector3 target);
}
