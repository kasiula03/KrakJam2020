using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private ActionOnPlayer _actionOnPlayer;
    [SerializeField] private float _range;
    [SerializeField] private string _targetTag;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    private Transform _playerTarget;
    public Vector3 _target = Vector3.zero;
    public bool facingRight = true;
    private Rigidbody2D _rb;

    private int Direction => facingRight ? 1 : -1;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
       // _isGrounded = Physics2D.Linecast(transform.position, transform.position + Vector3.down * 3, LayerMask.GetMask("Default"));
        if (_target == Vector3.zero)
        {
            FindTarget();
        }
        if(_target != Vector3.zero)
        {
            MoveToTarget();
        }
    }

    private void FindTarget()
    {
        List<RaycastHit2D> hits = Physics2D.RaycastAll(transform.position, Vector2.right * Direction, _range)
            .ToList().Where(hit => hit.transform.gameObject != gameObject && hit.transform.tag == _targetTag).ToList();
        if (hits.Count > 0)
        {
            Debug.Log("PlayerTarget");
            _playerTarget = hits[0].transform;
            _target = hits[0].transform.position;
        }
        else
        {
            RandomTarget();
        }
      
        facingRight = _target.x > transform.position.x;
    }

    private void RandomTarget()
    {
        Debug.Log("Random");
        int direction = Random.Range(0, 2);
        _target = transform.position + Vector3.one * (direction == 0 ? -1 : 1) * _range;
        _target.y = 0;
    }

    private void MoveToTarget()
    {
        float step = _speed * Time.deltaTime;
        float distance = Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(_target.x, 0));

        if(_playerTarget != null && distance < 0.2f)
        {
            //todo: action of player
            _actionOnPlayer.DoAction(_playerTarget.position);
        }
        else if(distance < 0.2f)
        {
            Debug.Log("Finish");
            _target = Vector3.zero;
        }
        else
        {
            transform.position = transform.position + new Vector3(Direction, 0, 0) * step;
        }
    }
}
