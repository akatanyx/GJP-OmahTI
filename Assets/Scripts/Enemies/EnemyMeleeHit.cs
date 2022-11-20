using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //hit = true;
            collision.GetComponent<HealthManager>().DealDamage(10, tag);
            //StartCoroutine(DisableMovementAfterAttack());
        }
    }
}
