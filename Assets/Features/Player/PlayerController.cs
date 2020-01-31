using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Unity objects
    public Transform playerPostiion;

    //Player Parameters
    public float playerMoveSpeed    = 7f;
    public float playerMoveMaxSpeed = 3f;
    public float jumpForce          = 5f;

    //Control keys
    public string moveLeftKeyCode   =   "a";
    public string moveRightKeyCode  =   "d";
    public string jumpKeyCode       =   "w";

    //private variables
    public delegate void PlayerEvent();

    private float _deltaTime;
    private List<EventConfig> _playerEvents = new List<EventConfig>();
    private Rigidbody2D _rb;
    private bool _isGrounded = false;


    //Helper structures
    public enum TypeEvent
    {
        Key,
        Up,
        Down
    }
    public struct EventConfig
    {
        public PlayerEvent eventFunction;
        public TypeEvent eventType;
        public string keyCode;
        public EventConfig(PlayerEvent _eventFunction, TypeEvent _eventType, string _keyCode)
        {
            eventFunction = _eventFunction;
            eventType = _eventType;
            keyCode = _keyCode;
        }
    }


    // Unity functions
    void Start()
    {
        InitPlayerEvents();
        _rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        _deltaTime = Time.deltaTime;
        PlayerMovement();
      
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        _isGrounded = true;
    }


    //Public functions
    public void InitPlayerEvents()
    {
        _playerEvents.Add(new EventConfig(MoveLeft, TypeEvent.Key, moveLeftKeyCode));
        _playerEvents.Add(new EventConfig(MoveRight, TypeEvent.Key, moveRightKeyCode));
        _playerEvents.Add(new EventConfig(Jump, TypeEvent.Down, jumpKeyCode));
    }

    //Private functions
    private void PlayerMovement()
    {
        foreach (var playerEvent in _playerEvents)
        {
            switch (playerEvent.eventType)
            {
                case TypeEvent.Down:
                    if (Input.GetKeyDown(playerEvent.keyCode))
                    {
                        playerEvent.eventFunction.Invoke();
                    }
                    break;
                case TypeEvent.Key:
                    if (Input.GetKey(playerEvent.keyCode))
                    {
                        playerEvent.eventFunction.Invoke();
                    }
                    break;
                case TypeEvent.Up:
                    if (Input.GetKeyUp(playerEvent.keyCode))
                    {
                        playerEvent.eventFunction.Invoke();
                    }
                    break;
            }
        }
    }

    private void Jump()
    {
        if ( _isGrounded)
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _isGrounded = false;
        }
    }
    private void MoveLeft()
    {
        if (_isGrounded)
        {
            if (_rb.velocity.magnitude > playerMoveMaxSpeed)
            {
                _rb.velocity = _rb.velocity.normalized * playerMoveMaxSpeed;
            }
            else
            {
                _rb.AddForce(Vector2.left * _deltaTime * playerMoveSpeed, ForceMode2D.Impulse);
            }
        }
    }
    private void MoveRight()
    {
        if (_isGrounded)
        {
            if (_rb.velocity.magnitude > playerMoveMaxSpeed)
            {
                _rb.velocity = _rb.velocity.normalized * playerMoveMaxSpeed;
            }
            else
            {
                _rb.AddForce(Vector2.right * _deltaTime * playerMoveSpeed, ForceMode2D.Impulse);
            }
        }
    }
}
