﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    public float speed = 2f;
    public float maxDistance = 10f;

    private float _deltaTime;
    private Vector2 _startPosition;
    private string _sourceTag;
    private string _enemyTag;
    private float _direction;
    private Vector3 _target;

    void Start()
    {
        _startPosition = transform.position;
    }
    void Update()
    {
        _deltaTime = Time.deltaTime;
        transform.position += (_target - transform.position).normalized * speed * _deltaTime;

        var distance = Vector2.Distance(_startPosition, transform.position);
        if (distance > maxDistance)
            Destroy(gameObject);
    }

    public void Setup(string sourceTag, string enemyTag, float direction, Vector3 target)
    {
        _sourceTag = sourceTag;
        _enemyTag = enemyTag;
        _direction = direction;
        _target = target;
        //  transform.rotation.SetEulerAngles(new Vector3(0, 0, direction * 45));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.tag != _sourceTag)
        {
            if (col.collider.gameObject.tag == _enemyTag)
            {
                EnemyKillingCondition killingCondition = col.gameObject.GetComponent<EnemyKillingCondition>();
                if (killingCondition != null && killingCondition.IsKillable())
                {
                    Destroy(col.collider.gameObject);
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
