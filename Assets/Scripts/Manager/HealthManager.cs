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
    Rigidbody2D rb;
    public SpriteRenderer sprite;
    bool flashing;
    Color flash, defaultColor;
    
    void Start ()
    {
        healthPoints = maxHealthPoints;
        col = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        flash = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.3f);
        //defaultColor = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public void DealDamage(int damage, string damageType, bool canKnockback = false, float knockbackForce = 0, float localScale = 1)
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
                StartCoroutine(StartKnockback(knockbackForce, localScale));
            }
        }
    }

    IEnumerator Dying()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            sprite.color = new Color(0, 0, 0, i);
        }
        yield return new WaitForSeconds(0.1f);
        col.isTrigger = false;
        rb.gravityScale = 2;
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

    //void Knockback(float knockbackForce)
    //{
        
    //    //CharacterMovement enemyMovement = GetComponent<CharacterMovement>();

    //    //Matikan Movement Karakter biar bisa knockback karena bermasalah dengan fixedupdate
    //    //StartCoroutine(StartKnockback(knockbackForce));

    //    //Kalkulasi
    //    //Vector2 direction = (transform.position - transform.position);
    //}

    IEnumerator StartKnockback(float knockbackForce, float localScale)
    {
        if(GetComponent<CharacterMovement>() != null)
        {
            CharacterMovement movement = GetComponent<CharacterMovement>();
            movement.enabled = false;
            rb.velocity = Vector2.zero;
            Vector2 direction = new Vector2(-localScale, 0);
            direction = direction.normalized * knockbackForce;
            rb.AddForce(direction, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.5f);
            //Debug.Log("tes0");
            movement.enabled = true;
        }
        else
        {
            EnemyMovement movement = GetComponent<EnemyMovement>();
            movement.enabled = false;
            Vector2 direction = new Vector2(-transform.localScale.x, 0);
            direction = direction.normalized * knockbackForce;
            rb.AddForce(direction, ForceMode2D.Impulse);
            yield return new WaitForSeconds(.5f);
            movement.enabled = true;
        }
        
    }
}
