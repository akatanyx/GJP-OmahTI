using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : EnemyCharacter
{
    AIMeleeAttack meleeAttack;
    AIRangedAttack rangedAttack;
    [SerializeField] float switchTime = 5, rangedSpeed, meleeSpeed;
    bool isRanged;
    protected override void Initialization()
    {
        base.Initialization();
        meleeAttack = GetComponent<AIMeleeAttack>();
        rangedAttack = GetComponent<AIRangedAttack>();
        meleeAttack.enabled = false;
        rangedAttack.enabled = true;
        StartCoroutine(SwitchTimer());
    }
   
    // Update is called once per frame
    void Update()
    {
        //SwitchMode();
    }

    void SwitchMode()
    {
        if (isRanged)
        {
            health.charType = HealthManager.characterType.immuneRanged;
            meleeAttack.enabled = false;
            rangedAttack.enabled = true;
            health.sprite.color = Color.green;
            enemyMovement.minDistance = 5f;
            enemyMovement.maxSpeed = rangedSpeed;
            isRanged = false;
        }
        else
        {
            health.charType = HealthManager.characterType.immuneMelee;
            meleeAttack.enabled = true;
            rangedAttack.enabled = false;
            health.sprite.color = Color.red;
            enemyMovement.minDistance = 1f;
            enemyMovement.maxSpeed = meleeSpeed;
            isRanged = true;
        }
    }

    IEnumerator SwitchTimer()
    {
        yield return new WaitForSeconds(.1f);
        SwitchMode();
        while(health.healthPoints > 0)
        {
            yield return new WaitForSeconds(switchTime);
            SwitchMode();
        }
    }
}
