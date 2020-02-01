﻿using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [Inject] private readonly PlayerAbilitiesLogic _playerAbilitiesLogic;
    [SerializeField] private Transform _checkOnGround;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private FakeAnimationThatIWillUseInsteadOfUsingBuiltinUnityAnimator _anim;
    
    //Player Parameters
    public float playerMoveSpeed    = 7f;
    public Vector2 playerMoveMaxSpeed = new Vector2(3f, 5f);
    public float jumpForce          = 5f;
    public float jetpackForce       = 15f;
    public float maxTimeFly = 2f;

    public PlayerHealth PlayerHealth;

    //Prefabs to spawn
    public GameObject BulletPrefab;
    public GameObject JetPackParticlePrefab;

    //Control keys
    public string moveLeftKeyCode   =   "a";
    public string moveRightKeyCode  =   "d";
    public string jumpKeyCode       =   "w";
    public string fireKeyCode = "space";
    public string jetpackKeyCode    =   "v";

    //private variables
    public delegate void PlayerEvent();

    private List<EventConfig> _playerEvents = new List<EventConfig>();
    private Rigidbody2D _rb;
    private bool _isGrounded = false;
    private float _endTimeFly = 0;
    private GameObject _jetpackParticle = null;
    
    private int _direction = 1;

    public Vector2 VerticalDirection
    {
        get => _direction == -1 ? Vector2.left : Vector2.right;
    }


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
    
    //void Update()

    FakeAnimationThatIWillUseInsteadOfUsingBuiltinUnityAnimator.States CalculateState()
    {
        if (!_isGrounded)
        {
            return FakeAnimationThatIWillUseInsteadOfUsingBuiltinUnityAnimator.States.JUMP_1;
        }

        if (left || right)
        {
            return FakeAnimationThatIWillUseInsteadOfUsingBuiltinUnityAnimator.States.RUN;
        }

        return FakeAnimationThatIWillUseInsteadOfUsingBuiltinUnityAnimator.States.IDLE;
        
    }

    private bool left = false;
    private bool right = false;

    void CollectInput()
    {
        left = Input.GetKey(moveLeftKeyCode);
        right = Input.GetKey(moveRightKeyCode);
    }
    
    void Update()
    {
        _isGrounded = CalculateIsGrounded();
        CollectInput();
        
        _anim.SetAnimationState(CalculateState());
        
        bool movementThisFrame = false;
        PlayerMovement();
        if (movementThisFrame == false)
        {
            ApplyBraking();
        }
    }
 
    void OnCollisionEnter2D(Collision2D col)
    {
        if(_jetpackParticle != null)
        {
            Destroy(_jetpackParticle);
        }
        
        if (col.collider.gameObject.tag == "healthPoint")
        {
            AddHealth(1);
            Destroy(col.gameObject);
        }
        else if (col.collider.gameObject.tag == "asteroid")
        {
            SubHealth(1);
            Destroy(col.gameObject);
        }
    }


    //Public functions
    public void InitPlayerEvents()
    {
        //_playerEvents.Add(new EventConfig(MoveLeft, TypeEvent.Key, moveLeftKeyCode));
        //_playerEvents.Add(new EventConfig(MoveRight, TypeEvent.Key, moveRightKeyCode));
        _playerEvents.Add(new EventConfig(Abilities.BindableReason.JumpButtonPressed,
            TypeEvent.Key,
            jumpKeyCode));
        _playerEvents.Add(new EventConfig(Abilities.BindableReason.FireButtonPressed,
            TypeEvent.Down,
            fireKeyCode));
        _playerEvents.Add(new EventConfig(Fly, TypeEvent.Key, jetpackKeyCode));
        _playerEvents.Add(new EventConfig(StartFly, TypeEvent.Key, jetpackKeyCode));
    }

    //Private functions
    private void PlayerMovement()
    {
        if(left) MoveLeft();
        if(right) MoveRight();
        
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
        GameObject newBox = Instantiate(BulletPrefab);
        newBox.transform.position = new Vector2(transform.position.x+1.5f * _direction, transform.position.y);
        if(_direction == -1)
            newBox.transform.Rotate(0, 0,180);
    }

    public void Jump()
    {
        if ( _isGrounded)
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _isGrounded = false;
        }
    }

    public void SubHealth (int damage)
    {
        PlayerHealth.SubHealth(damage);
        if (PlayerHealth.CurrentHealthy <= 0)
            Destroy(gameObject);
    }
    public void AddHealth(int additionalHealth)
    {
        PlayerHealth.AddHealth(additionalHealth);
    }


    private void StartFly()
    {
        if (_isGrounded)
        {
            _endTimeFly = Time.time + maxTimeFly;
            _jetpackParticle = Instantiate(JetPackParticlePrefab);
            _jetpackParticle.transform.parent = gameObject.transform;
            _jetpackParticle.transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
        }
    }
    private void Fly()
    {
        if (Time.time < _endTimeFly)
        {
            if (_rb.velocity.magnitude > playerMoveMaxSpeed.magnitude)
                _rb.velocity = _rb.velocity.normalized * playerMoveMaxSpeed;
            else
                _rb.AddForce(Vector2.up * Time.deltaTime * jetpackForce, ForceMode2D.Impulse);
        }
        else if (_jetpackParticle != null)
        {
            Destroy(_jetpackParticle);
        }
    }

    private void MoveLeft()
    {
        _direction = -1;
        _spriteRenderer.flipX = true;
        VerticalMovement(Vector2.left);
    }
    
    private void MoveRight()
    {
        _direction = 1;
        _spriteRenderer.flipX = false;
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
        if (_isGrounded && !left && !right)
        {
            float newX = _rb.velocity.x * .9f;
            _rb.velocity = new Vector2(newX, _rb.velocity.y); 
        }
    }

    private bool CalculateIsGrounded()
    {
        if(Physics2D.Linecast(this.transform.position, _checkOnGround.position, _layerMask))
        {
            return true;
        }

        return false;
    }
    
}
