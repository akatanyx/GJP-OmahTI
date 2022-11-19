using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : Character
{
    private bool isAttacking, canAttack = true;


    #region Ranged Attack

    public GameObject bulletSpawner;
    private int rangedAttackTime;
    [SerializeField]
    private int maxRangedAttackTime;
    [SerializeField]
    private float rangedAttackCooldown;

    public ObjectPooling pooler { get; set; }


    #endregion
    private void Start()
    {
        pooler = GetComponent<ObjectPooling>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canAttack || isDashing)
            return;
        StartAttack();
    }

    void StartAttack()
    {
        if (inputManager.AttackPressed())
        {
            anim.SetBool("AttackRanged", true);
            isAttacking = true;
        }
        else if (inputManager.AttackKeyUp())
            StartCoroutine(RangedAttackCooldown());
        //else
        //{
        //    StartCoroutine(RangedAttackCooldown());
        //}
    }

    void SpawnBullet()
    {
        rangedAttackTime++;
        spawnProjectile(bulletSpawner.transform.position);
        if(rangedAttackTime == maxRangedAttackTime)
        {
            StartCoroutine(RangedAttackCooldown());
        }
    }

    IEnumerator RangedAttackCooldown()
    {
        canAttack = false;
        isAttacking = false;
        rangedAttackTime = 0;
        anim.SetBool("AttackRanged", false);
        yield return new WaitForSeconds(rangedAttackCooldown);
        canAttack = true;
    }

    void spawnProjectile(Vector2 spawnPosition)
    {
        GameObject projectilePooled = pooler.GetObjectFromPool();
        projectilePooled.transform.position = spawnPosition;
        projectilePooled.SetActive(true);

        Projectile projectile = projectilePooled.GetComponent<Projectile>();
        Vector2 newDirection = flip.isFlipped ? -transform.right : transform.right;

        projectile.SetDirection(newDirection, transform.rotation);
        
    }
}
