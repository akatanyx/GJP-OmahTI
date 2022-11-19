using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealthPoints;
    //[HideInInspector] 
    public int healthPoints;
    Collider2D col;
    SpriteRenderer sprite;
    bool flashing;
    Color flash;

    void Start()
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

    public void DealDamage(int damage)
    {
        healthPoints -= damage;
        StartCoroutine(Blinking());
        if (healthPoints <= 0)
        {
            StartCoroutine(Dying());
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
}
