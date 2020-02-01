using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyFlyMovement : MonoBehaviour
{
    [SerializeField] private Vector3 _flyHeight;
    [SerializeField] private float _targetRange;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _shootCooldown;
    [SerializeField] private MovementAnimation _movementAnimation;
    //[SerializeField] private LayerMask _layerMask;

    private Transform _playerTarget;
    private Rigidbody2D _rb;
    private float _nextFire;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _movementAnimation.Animate();
    }

    public void Update()
    {
        if(_playerTarget == null)
        {
            FindTarget();
        }
        if (_playerTarget == null)
        {
            Float();
        }
        else
        {
            FlyToTarget();
            if (Time.time > _nextFire)
            {
                Shot();
            }
        }
    }

    private void Shot()
    {
        _nextFire = Time.time + _shootCooldown;
        Bullet bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
     
        Rigidbody2D rg = bullet.GetComponent<Rigidbody2D>();
        Vector3 direction = bullet.transform.position - _playerTarget.position;
        float dir = direction.normalized.x < 0 ? 1 : -1;
        Debug.Log(dir);
        bullet.Setup("Enemy", "Player", dir, _playerTarget.position);
        // rg.AddForce(direction.normalized * Vector2.one);
       // rg.velocity = dir * 3 * Vector2.one;
    }

    private void FindTarget()
    {
        List<Collider2D> targets = Physics2D.OverlapCapsuleAll(transform.position, Vector2.one * _targetRange, CapsuleDirection2D.Horizontal, 360)
            .Where(target => target.tag == "Player").ToList();
       
        _playerTarget = targets.Count > 0 ? targets[0].transform : null;

    }

    private void Float()
    {
        if(transform.position.y < _flyHeight.y)
        {
            _rb.AddForce(Vector2.up * Time.deltaTime * 10, ForceMode2D.Impulse);
        }
    }

    private void FlyToTarget()
    {
        int direction = transform.position.x < _playerTarget.position.x ? 1 : -1;
        if (transform.position.y < _flyHeight.y)
        {
            _rb.AddForce((Vector2.up * 10 + (direction * Vector2.right)) * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
}
