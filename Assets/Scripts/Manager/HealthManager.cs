using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealthPoints;
    //[HideInInspector] 
    public int healthPoints;
    public enum characterType
    {
        basic,
        immuneMelee,
        immuneRanged
    }
    public characterType charType;
    Collider2D col;
    SpriteRenderer sprite;
    bool flashing;
    Color flash;
    
    void Start ()
    {
        healthPoints = maxHealthPoints;
        col = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        flash = new Color(1, 1, 1, 0.3f);
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public void DealDamage(int damage, string damageType, bool canKnockback = false, float knockbackForce = 0)
    {
        if(damageType == "Melee" && charType == characterType.immuneMelee)
        {
            return;
        }
        else if(damageType == "Ranged" && charType == characterType.immuneRanged)
        {
            return;
        }
        else
        {
            healthPoints -= damage;
            StartCoroutine(Blinking());
            if (healthPoints <= 0)
            {
                StartCoroutine(Dying());
            }
            if (canKnockback)
            {
                Knockback(knockbackForce);
            }
        }
    }

    IEnumerator Dying()
    {
        col.enabled = false;
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            sprite.color = new Color(0, 0, 0, i);
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    IEnumerator Blinking()
    {
        float timer = 0;
        flashing = true;
        while (timer < 1)
        {
            sprite.color = flash;
            yield return new WaitForSeconds(0.1f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            timer += .4f;
        }
        if (timer > 1)
        {
            flashing = false;
        }
        yield return null;

    }

    void Knockback(float knockbackForce)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //CharacterMovement enemyMovement = GetComponent<CharacterMovement>();

        //Matikan Movement Karakter biar bisa knockback karena bermasalah dengan fixedupdate
        StartCoroutine(StartKnockback());

        //Kalkulasi
        //Vector2 direction = (transform.position - transform.position);
        Vector2 direction = new Vector2 (-transform.localScale.x, 0);
        direction = direction.normalized * knockbackForce;
        Debug.Log(direction + " " + rb);
        rb.AddForce(direction, ForceMode2D.Impulse);
    }

    IEnumerator StartKnockback()
    {
        if(GetComponent<CharacterMovement>() != null)
        {
            CharacterMovement movement = GetComponent<CharacterMovement>();
            movement.enabled = false;
            yield return new WaitForSeconds(0.5f);
            movement.enabled = true;
        }
        else
        {
            EnemyMovement movement = GetComponent<EnemyMovement>();
            movement.enabled = false;
            yield return new WaitForSeconds(.5f);
            movement.enabled = true;
        }
        
    }
}
