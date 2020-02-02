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
    [SerializeField] private int _bulletPerWave;
    // public Bullet LaserBullet;

    private Transform _playerTarget;
    private float nextAttack;
    private float _nextFire;
    private bool _block;
    private int _bullet;
    private int unlockTime = 10;
    private float _nextWave;
    void Update()
    {

        if(_playerTarget != null)
        {
            if(_block && Time.time > _nextWave)
            {
                _block = false;
            }
            if(_block)
            {
                return;
            }

            if (Time.time > _nextFire)
            {
                Shot();
                _bullet++;
                _nextFire = Time.time + _shootCooldown;
            }
            if(_bullet > 20)
            {
                _block = true;
                _bullet = 0;
                _nextWave = Time.time + unlockTime;
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
