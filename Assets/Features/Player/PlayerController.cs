using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [Inject] private readonly PlayerAbilitiesLogic _playerAbilitiesLogic;
    [SerializeField] private Transform _checkOnGround;
    [SerializeField] private LayerMask _layerMask;
    
    //Player Parameters
    public float playerMoveSpeed    = 7f;
    public Vector2 playerMoveMaxSpeed = new Vector2(3f, 5f);
    public float jumpForce          = 5f;

    //Control keys
    public string moveLeftKeyCode   =   "a";
    public string moveRightKeyCode  =   "d";
    public string jumpKeyCode       =   "w";
    public string fireKeyCode = "space";

    //private variables
    public delegate void PlayerEvent();

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
        public bool resolve;
        public PlayerEvent eventFunction;
        public Abilities.BindableReason resolver;
        public TypeEvent eventType;
        public string keyCode;
        public EventConfig(PlayerEvent _eventFunction, TypeEvent _eventType, string _keyCode)
        {
            eventFunction = _eventFunction;
            eventType = _eventType;
            keyCode = _keyCode;
            resolve = false;
            resolver = Abilities.BindableReason.FireButtonPressed;
        }

        public EventConfig(Abilities.BindableReason bind, TypeEvent eventType, string keyCode)
        {
            resolver = bind;
            this.eventType = eventType;
            this.keyCode = keyCode;
            eventFunction = null;
            resolve = true;
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
        this._isGrounded = CalculateIsGrounded();
        
        bool movementThisFrame = false;
        PlayerMovement();
        if (movementThisFrame == false)
        {
            ApplyBraking();
        }
    }
    
    /*
    void OnCollisionEnter2D(Collision2D col)
    {
        _isGrounded = true;
    }
    */

    //Public functions
    public void InitPlayerEvents()
    {
        _playerEvents.Add(new EventConfig(MoveLeft, TypeEvent.Key, moveLeftKeyCode));
        _playerEvents.Add(new EventConfig(MoveRight, TypeEvent.Key, moveRightKeyCode));
        
        _playerEvents.Add(new EventConfig(Abilities.BindableReason.JumpButtonPressed,
            TypeEvent.Down,
            jumpKeyCode));
        _playerEvents.Add(new EventConfig(Abilities.BindableReason.FireButtonPressed,
            TypeEvent.Down,
            fireKeyCode));
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
                        playerEvent.eventFunction?.Invoke();
                        if (playerEvent.resolve)
                        {
                            Abilities.BindedActions[_playerAbilitiesLogic.GetProperty(playerEvent.resolver).Value]
                                .Invoke((this));
                        }
                    }
                    break;
                case TypeEvent.Key:
                    if (Input.GetKey(playerEvent.keyCode))
                    {
                        playerEvent.eventFunction?.Invoke();
                        if (playerEvent.resolve)
                        {
                            Abilities.BindedActions[_playerAbilitiesLogic.GetProperty(playerEvent.resolver).Value]
                                .Invoke((this));
                        }
                    }
                    break;
                case TypeEvent.Up:
                    if (Input.GetKeyUp(playerEvent.keyCode))
                    {
                        playerEvent.eventFunction?.Invoke();
                        if (playerEvent.resolve)
                        {
                            Abilities.BindedActions[_playerAbilitiesLogic.GetProperty(playerEvent.resolver).Value]
                                .Invoke((this));
                        }
                    }
                    break;
            }
        }
    }

    public void Fire()
    {
        Debug.Log("FIRE!!");
    }

    public void Jump()
    {
        if ( _isGrounded)
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _isGrounded = false;
        }
    }

    
    private void MoveLeft()
    {
        VerticalMovement(Vector2.left);
    }
    
    private void MoveRight()
    {
        VerticalMovement(Vector2.right);
    }
    
    private void VerticalMovement(Vector2 direction)
    {
        if (_isGrounded)
        {
            _rb.velocity += (direction * playerMoveSpeed * Time.deltaTime);
        }
        else
        {
            _rb.velocity += (direction * playerMoveSpeed * .01f * Time.deltaTime);
        }

        if (Mathf.Abs(_rb.velocity.x) > playerMoveMaxSpeed.x)
        {
            float newX = _rb.velocity.x > 0 ? playerMoveMaxSpeed.x : -playerMoveMaxSpeed.x;
            
            _rb.velocity = new Vector2(newX, _rb.velocity.y);
        }
    }

    private void ApplyBraking()
    {
        if (_isGrounded)
        {
            float newX = _rb.velocity.x * .9f;
            _rb.velocity = new Vector2(newX, _rb.velocity.y); 
        }
    }

    private bool CalculateIsGrounded()
    {
        //var k = new List<RaycastHit2D>();
        if(Physics2D.Linecast(this.transform.position, _checkOnGround.position, _layerMask))
        //if(Physics2D.Linecast(this.transform.position, _checkOnGround.position, _layerMask, k))
        {
            return true;
        }

        return false;
    }
}
