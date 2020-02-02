using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float laserTime = 1f;
    public Bullet LaserBullet;

    private string _sourceTag;
    private string _enemyTag;
    private float _startTime;

    public void Setup(string sourceTag, string enemyTag, float direction)
    {
        _sourceTag = sourceTag;
        _enemyTag = enemyTag;

        _startTime = Time.time;
    }

    private float lastFireTime = -10;
    private float reloadGunTime = 0.01f;
    private int _direction = 1;
    public float laserSpeed = 2f;

    public void ChangeDirection(int direction)
    {
        _direction = direction;
    }
    void Update()
    {
        if(Time.time > _startTime + laserTime)
        {
            Destroy(gameObject);
        }

        if (Time.time > lastFireTime + reloadGunTime)
        {
            Bullet newBox = Instantiate(LaserBullet);
            newBox.transform.position = new Vector2(transform.position.x, transform.position.y);
            newBox.Setup("Player", "Enemy", _direction);
            if (_direction == -1)
                newBox.transform.Rotate(0, 0, 180);
            lastFireTime = Time.time;
        }


    }

}
