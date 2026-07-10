using UnityEngine;


public class LaserDamage : MonoBehaviour
{
    public int damage = 1;
    public float damageInterval = 0.6f; // seconds between hits while inside beam

    private float _nextHitTime;

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (Time.time < _nextHitTime) return;

        other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        _nextHitTime = Time.time + damageInterval;
    }

    // Also trigger immediately on entry so first contact feels instant
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        _nextHitTime = Time.time + damageInterval;
    }
}