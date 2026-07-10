using UnityEngine;


public class Trap : MonoBehaviour
{
    public int damage = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
    }
}