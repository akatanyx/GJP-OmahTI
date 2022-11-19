using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : Character
{
    private bool isAttacking, canAttack = true;
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
    private Collider2D meleeCol;

    #endregion
    private void Start()
    {
        pooler = GetComponent<ObjectPooling>();
        //meleeCol = meleeAttackObject.GetComponent<Collider2D>();
        //meleeCol.enabled = false;
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
