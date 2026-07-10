using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAnimator : MonoBehaviour
{
    private Animator _anim;
    private PlayerHealth _health;

    // Cache hash IDs — faster than passing strings every frame
    private static readonly int IsFlying = Animator.StringToHash("isFlying");
    private static readonly int HurtTrigger = Animator.StringToHash("Hurt");
    private static readonly int DieTrigger = Animator.StringToHash("Die");

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _health = GetComponent<PlayerHealth>();
    }

    void OnEnable()
    {
        _health.OnDamaged += PlayHurt;
        _health.OnDied += PlayDie;
    }

    void OnDisable()
    {
        _health.OnDamaged -= PlayHurt;
        _health.OnDied -= PlayDie;
    }

    void Update()
    {
        // Idle vs Flying — driven by whether Space is held
        bool flying = Keyboard.current != null && Keyboard.current.spaceKey.isPressed;
        _anim.SetBool(IsFlying, flying);
    }

    void PlayHurt() => _anim.SetTrigger(HurtTrigger);
    void PlayDie() => _anim.SetTrigger(DieTrigger);
}