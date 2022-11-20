using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityChecker : MonoBehaviour
{
    CharacterAttack characterAttack;
    int enemyInProximity;
    private void Start()
    {
        characterAttack = GetComponentInParent<CharacterAttack>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyInProximity++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (enemyInProximity < 0)
                enemyInProximity = 0;
            enemyInProximity--;
        }
    }

    private void Update()
    {
        if (enemyInProximity > 0)
            characterAttack.isEnemyInProximity = true;
        else
            characterAttack.isEnemyInProximity = false;
    }
}
