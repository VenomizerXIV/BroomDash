using System.Collections;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHP = 3;
    public float iFrameDuration = 1.2f;

    public int CurrentHP { get; private set; }
    public bool IsAlive { get; private set; } = true;

    public event System.Action OnDamaged;
    public event System.Action OnDied;

    private float _iFrameTimer;
    private SpriteRenderer _sr;

    void Awake()
    {
        CurrentHP = maxHP;
        _sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (_iFrameTimer > 0f) _iFrameTimer -= Time.deltaTime;
    }

    public void TakeDamage(int amount)
    {
        if (!IsAlive) return;
        if (_iFrameTimer > 0f) return;

        CurrentHP = Mathf.Max(CurrentHP - amount, 0);
        _iFrameTimer = iFrameDuration;
        OnDamaged?.Invoke();

        if (CurrentHP <= 0) Die();
        else StartCoroutine(FlashRoutine());
    }

    void Die()
    {
        IsAlive = false;
        OnDied?.Invoke();
    }

    IEnumerator FlashRoutine()
    {
        if (_sr == null) yield break;
        for (int i = 0; i < 4; i++)
        {
            _sr.color = new Color(1f, 0.2f, 0.2f, 0.5f);
            yield return new WaitForSeconds(0.12f);
            _sr.color = Color.white;
            yield return new WaitForSeconds(0.12f);
        }
    }
}