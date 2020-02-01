using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2f;
    public float maxDistance = 10f;

    private float _deltaTime;
    private Vector2 _startPosition;
    private string _sourceTag;
    private string _enemyTag;
    private float _direction;

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

    public void Setup(string sourceTag, string enemyTag, float direction)
    {
        _sourceTag = sourceTag;
        _enemyTag = enemyTag;
        _direction = direction;
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
                    Debug.Log("Killable");
                    PlayerHealth health = col.gameObject.GetComponent<PlayerHealth>();
                    if (health != null)
                    {
                        health.SubHealth(1);
                    }
                    else
                    {
                        Destroy(col.collider.gameObject);
                    }
                }
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

}
