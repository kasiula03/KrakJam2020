using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private ActionOnPlayer _actionOnPlayer;
    [SerializeField] private MovementAnimation _movementAnimation;
    [SerializeField] private EnemyProperty _property;
    [SerializeField] private float _range;
    [SerializeField] private string _targetTag;
    [SerializeField] private float _speed;
    [SerializeField] private bool _idleMovement;
    private Transform _playerTarget;
    public Vector3 _target = Vector3.zero;
    public bool facingRight = true;

    private Sequence _animation;

    private int Direction => facingRight ? 1 : -1;


    private void Update()
    {
        if (_target == Vector3.zero)
        {
            FindTarget();
        }
        if(_target != Vector3.zero && !_property.isBlocked)
        {
            MoveToTarget();
        }
        if(_property.isBlocked)
        {
            if (_movementAnimation != null && _animation != null)
            {
                _animation.Pause();
                _animation = null;
                _movementAnimation.isAnimating = false;
            }
        }
    }

    private void FindTarget()
    {
        Vector3 centerOfEnemy = GetComponent<BoxCollider2D>().bounds.center;
        RaycastHit2D[] allHits =  Physics2D.RaycastAll(centerOfEnemy, Vector2.right * Direction, _range);
        List<RaycastHit2D> hits = allHits
            .ToList().Where(hit => hit.transform.gameObject != gameObject && hit.transform.tag == _targetTag).ToList();
        if (hits.Count > 0)
        {
            Debug.Log("Player");
            _playerTarget = hits[0].transform;
            _target = hits[0].transform.position;
            facingRight = _target.x > transform.position.x;
        }
        else if(_idleMovement)
        {
            RandomTarget();
            facingRight = _target.x > transform.position.x;
        }

        SetProperties();
    }

    private void SetProperties()
    {
        _property.targetInRange = _playerTarget != null;
        _property.facingRight = facingRight;
    }

    private void RandomTarget()
    {
        int direction = Random.Range(0, 2);
        _target = transform.position + Vector3.one * (direction == 0 ? -1 : 1) * _range;
        _target.y = 0;
    }

    private void MoveToTarget()
    {
        float step = _speed * Time.deltaTime;
        float distance = Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(_target.x, 0));
        if(_playerTarget != null)
        {
            distance = Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(_playerTarget.position.x, 0));
        }

        if(_playerTarget != null && _actionOnPlayer != null && distance < 1f)
        {
            //todo: action of player
            _actionOnPlayer.DoAction(_playerTarget.position);
        }
        else if(distance < 1f)
        {
            Debug.Log("Finish");
            _target = Vector3.zero;
           
            if (_movementAnimation != null)
            {
                _animation.Pause();
                _animation = null;
                _movementAnimation.isAnimating = false;
            }
        }
        else
        {
            if(_movementAnimation != null && !_movementAnimation.isAnimating)
            {
                _animation = _movementAnimation.Animate();
                _movementAnimation.isAnimating = true;
            }
            transform.position = transform.position + new Vector3(Direction, 0, 0) * step;
        }
    }
}
