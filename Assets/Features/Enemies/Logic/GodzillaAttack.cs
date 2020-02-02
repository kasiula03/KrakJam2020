using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GodzillaAttack : MonoBehaviour
{
    public float laserTime = 1f;
    [SerializeField] private LaserBullet _bulletPrefab;
    [SerializeField] private float _targetRange;
    [SerializeField] private float _shootCooldown;
    // public Bullet LaserBullet;

    private Transform _playerTarget;
    private float nextAttack;
    private float _nextFire;

 

    void Update()
    {

        if(_playerTarget != null)
        {
            if (Time.time > _nextFire)
            {
                Shot();
                _nextFire = Time.time + _shootCooldown;
            }
        }
        else
        {
            FindTarget();
        }
    }


    private void Shot()
    {
        LaserBullet bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);

        Rigidbody2D rg = bullet.GetComponent<Rigidbody2D>();
        Vector3 direction = bullet.transform.position - _playerTarget.position;
        float dir = direction.normalized.x < 0 ? 1 : -1;
        bullet.Setup("Enemy", "Player", dir, _playerTarget.position);
         rg.AddForce(direction.normalized * Vector2.one);
        // rg.velocity = dir * 3 * Vector2.one;
    }

    private void FindTarget()
    {
        Collider2D[] dd = Physics2D.OverlapCapsuleAll(transform.position, Vector2.one * _targetRange, CapsuleDirection2D.Horizontal, 360);
        List<Collider2D> targets = Physics2D.OverlapCapsuleAll(transform.position, Vector2.one * _targetRange, CapsuleDirection2D.Horizontal, 360)
            .Where(target => target.tag == "Player").ToList();

        _playerTarget = targets.Count > 0 ? targets[0].transform : null;

    }
}
