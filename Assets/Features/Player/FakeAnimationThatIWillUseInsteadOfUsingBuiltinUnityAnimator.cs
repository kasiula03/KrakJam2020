using UnityEngine;

public class FakeAnimationThatIWillUseInsteadOfUsingBuiltinUnityAnimator : MonoBehaviour
{
    public enum States
    {
        IDLE,
        RUN,
        JUMP_1,
        JUMP_2,
        JUMP_3,
        DEAD,
        SHOOT,
    }

    [SerializeField] private SpriteRenderer _renderer;

    [SerializeField] private float _spd;

    [SerializeField] private Sprite forIdle;
    [SerializeField] private Sprite forDead;
    [SerializeField] private Sprite[] forJump;
    [SerializeField] private Sprite[] forRun;
    [SerializeField] private Sprite forShoot;

    private float _shootWait;
    private float _shootWaitSet;
    
    private States CurrState { get; set; } = States.IDLE;

    public void SetAnimationState(States state)
    {
        CurrState = state;
    }

    private void Update()
    {
        _shootWait -= Time.deltaTime;
        if (_shootWait > 0)
        {
            return;
        }
        
        switch (CurrState)
        {
            case States.IDLE: _renderer.sprite = forIdle;
                return;
            case States.DEAD: _renderer.sprite = forDead;
                return;
            case States.RUN: _renderer.sprite = GetRunSprite();
                return;
            case States.JUMP_1 : _renderer.sprite = forJump[0];
                return;
            case States.JUMP_2 : _renderer.sprite = forJump[1];
                return;
            case States.JUMP_3 : _renderer.sprite = forJump[2];
                return;
            case States.SHOOT : _renderer.sprite = GetShootSprite();
                return;
        }
    }

    Sprite GetRunSprite()
    {
        return forRun[Mathf.CeilToInt(Time.time*_spd) % forRun.Length];
    }

    Sprite GetShootSprite()
    {
        _shootWait = _shootWaitSet;
        return forShoot;
    }
}
