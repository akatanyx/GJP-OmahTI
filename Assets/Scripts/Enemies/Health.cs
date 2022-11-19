using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealthPoints;
    //[HideInInspector] 
    public int healthPoints;
    bool flashing;
    SpriteRenderer sprite;
    Collider2D col;
    Rigidbody2D rb;
    EnemyCharacter enemyCharacter;
    Color flash;
    void Start()
    {
        healthPoints = maxHealthPoints;
        enemyCharacter = GetComponent<EnemyCharacter>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        col.enabled = true;
        sprite.color = Color.white;
        flash = new Color(1, 1, 1, 0.3f);
    }


    public virtual void DealDamage(int amount, bool knockBack)
    {
        healthPoints -= amount;
        GetComponent<Animator>().SetTrigger("Attacked");
        if(healthPoints <=0)
        {
            StartCoroutine(Dying());
        }

        if (enemyCharacter.enType == EnemyCharacter.EnemyType.Ranged)
            GetComponent<AIRangedAttack>().nextFire = 1f;
        else
            GetComponent<AIMeleeAttack>().timeTillDoAction = 1f;
        
        Debug.Log(amount + "" + knockBack);
        if (knockBack)
        {
            if (enemyCharacter.facingLeft)
                rb.AddForce(new Vector2(50, 0));
            else
                rb.AddForce(new Vector2(-50, 0));
            //Debug.Log(rb.velocity);
        }
        StartCoroutine(Blinking());
        //InvokeRepeating("Flashing", 0, 0.1f);
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
        if(timer >1)
        {
            flashing = false;
        }
        yield return null;
        
    }
}
