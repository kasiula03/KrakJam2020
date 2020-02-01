using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2f;
    public float maxDistance = 10f;

    private float _deltaTime;
    private Vector2 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }
    void Update()
    {
        _deltaTime = Time.deltaTime;
        transform.position += transform.right * speed * _deltaTime;

        var distance = Vector2.Distance(_startPosition, transform.position);
        if (distance > maxDistance)
            Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.tag != "Player" && col.collider.gameObject.layer != 10)//layer - bullet
        {
            if (col.collider.gameObject.tag == "Enemy")
            {
                EnemyKillingCondition killingCondition = col.gameObject.GetComponent<EnemyKillingCondition>();
                if (killingCondition != null && killingCondition.IsKillable())
                {
                    Destroy(col.collider.gameObject);
                }
            }
            
            Destroy(gameObject);
        }
    }

}
