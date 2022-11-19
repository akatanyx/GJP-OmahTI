using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : Character
{
    private bool isAttacking, canAttack = true;
    public bool canRangedAttackWhileMove;
    public bool isEnemyInProximity;

    [SerializeField]
    private float AttackDowntime = 0.5f;

    #region Ranged Attack

    public GameObject bulletSpawner;
    private int rangedAttackTime;
    [SerializeField]
    private int maxRangedAttackTime = 4;

    public ObjectPooling pooler { get; set; }


    #endregion

    #region Melee Attack

    private int meleeAttackTime;
    [SerializeField]
    private int maxMeleeAttackTime = 3;
    [SerializeField]
    private GameObject meleeAttackObject;

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
            if (!isEnemyInProximity)
            {
                anim.SetBool("AttackRanged", true);
                if (!canRangedAttackWhileMove)
                    DisableMovementWhileAttack();
            }
            else
            {
                anim.SetBool("AttackMelee", true);
            }
            isAttacking = true;
        }
        else if (inputManager.AttackKeyUp())
        {
            StartCoroutine(AttackCooldown());
        }
    }
    IEnumerator AttackCooldown()
    {
        canAttack = false;
        isAttacking = false;
        rangedAttackTime = 0;
        meleeAttackTime = 0;
        anim.SetBool("AttackRanged", false);
        anim.SetBool("AttackMelee", false);

        if (!canRangedAttackWhileMove)
            EnableMovementAfterAttack();

        yield return new WaitForSeconds(AttackDowntime);
        canAttack = true;
    }


    #region Ranged Attack Function

    //Diassign ke animation
    void SpawnBullet()
    {
        rangedAttackTime++;
        spawnProjectile(bulletSpawner.transform.position);
        if(rangedAttackTime == maxRangedAttackTime)
        {
            StartCoroutine(AttackCooldown());
        }
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


    void DisableMovementWhileAttack()
    {
        if (jump.isGrounded)
        {
            movement.enabled = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void EnableMovementAfterAttack()
    {
        movement.enabled = true;
    }
    #endregion

    #region Melee Attack Function

    //Diassign ke animation
    void MeleeAttack()
    {
        meleeAttackTime++;
        if (meleeAttackTime == maxMeleeAttackTime)
        {
            StartCoroutine(AttackCooldown());
        }
    }

    //Diassign ke animation
    void ShowMeleeAttackCollider()
    {
        //meleeCol.enabled = true;
        meleeAttackObject.SetActive(true);
    }
    void UnShowMeleeAttackCollider()
    {
        //meleeCol.enabled = false;
        meleeAttackObject.SetActive(false);
    }
    #endregion
}
