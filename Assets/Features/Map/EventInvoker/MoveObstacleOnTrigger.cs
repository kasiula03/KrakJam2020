using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveObstacleOnTrigger : TriggerPoint
{
    [SerializeField] private Vector3 Distance;
    [SerializeField] private float MoveSpeed = 2f;

    public override void OnTriggerAction(Collision2D collider)
    {
        transform.DOMove(transform.position + Distance, MoveSpeed);
    }
}
