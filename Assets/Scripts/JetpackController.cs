using UnityEngine;
using UnityEngine.InputSystem;

public class JetpackController : MonoBehaviour
{
    [Header("Jetpack")]
    public float normalFlySpeed = 6f;   
    public float boostFlySpeed = 11f;  

    [Header("Fast Land")]
    public float fastLandForce = 20f;

    [Header("Speed Over Time")]
    public float startSpeed = 5f;
    public float maxSpeed = 16f;
    public float speedIncreaseRate = 0.08f;

    [Header("Boost Gauge")]
    public float maxBoostFuel = 100f;
    public float boostDrainRate = 35f;
    public float boostRechargeRate = 18f;

    [HideInInspector] public float boostFuel;
    [HideInInspector] public bool isBoosting;

    private Rigidbody2D _rb;
    private PlayerHealth _health;
    private float _currentSpeed;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _health = GetComponent<PlayerHealth>();
        boostFuel = maxBoostFuel;
        _currentSpeed = startSpeed;
    }

    void Update()
    {
        if (_health != null && !_health.IsAlive)
        {
            _rb.linearVelocity = Vector2.zero;
            _rb.gravityScale = 0f;
            return;
        }

        if (Time.timeScale == 0f)
        {
            isBoosting = false;
            return;
        }

        HandleBoost();
        HandleJetpack();
        HandleFastLand();
        HandleSpeed();
        ApplyHorizontal();
    }

    void HandleBoost()
    {
        // Boost only activates when BOTH Space AND Shift are held
        bool wantsBoost = Keyboard.current.spaceKey.isPressed &&
                          Keyboard.current.leftShiftKey.isPressed;

        isBoosting = wantsBoost && boostFuel > 0f;

        if (isBoosting)
            boostFuel = Mathf.Max(boostFuel - boostDrainRate * Time.deltaTime, 0f);
        else
            boostFuel = Mathf.Min(boostFuel + boostRechargeRate * Time.deltaTime, maxBoostFuel);
    }

    void HandleJetpack()
    {
        if (!Keyboard.current.spaceKey.isPressed) return;

        // Directly set vertical velocity — works regardless of gravity scale
        float targetY = isBoosting ? boostFlySpeed : normalFlySpeed;
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, targetY);
    }

    void HandleFastLand()
    {
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
            _rb.AddForce(Vector2.down * fastLandForce, ForceMode2D.Force);
    }

    void HandleSpeed()
    {
        _currentSpeed = Mathf.Min(_currentSpeed + speedIncreaseRate * Time.deltaTime, maxSpeed);
    }

    void ApplyHorizontal()
    {
        _rb.linearVelocity = new Vector2(_currentSpeed, _rb.linearVelocity.y);
    }
}