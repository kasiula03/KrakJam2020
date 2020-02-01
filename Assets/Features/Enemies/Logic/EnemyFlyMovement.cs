using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyFlyMovement : MonoBehaviour
{
    [SerializeField] private Vector3 _flyHeight;
    [SerializeField] private float _targetRange;
    //[SerializeField] private LayerMask _layerMask;

    private Transform _playerTarget;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
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
            Shot();
        }
    }

    private void Shot()
    {

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
            _rb.AddForce((Vector2.up + direction * Vector2.right) * Time.deltaTime * 10, ForceMode2D.Impulse);
        }
    }
}
