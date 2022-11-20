using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMeleeHit : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] bool canKnockback;

    [SerializeField] float knockbackForce;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<HealthManager>().DealDamage(damage, tag, canKnockback, knockbackForce);
        }
    }
}
