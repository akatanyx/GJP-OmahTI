using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : Character
{
    public bool isAttacking;
    private bool canAttack = true;
    public bool canRangedAttackWhileMove;
    public bool isEnemyInProximity;

    [SerializeField]
    private float AttackDowntime = 0.5f;

    #region Ranged Attack

    public GameObject bulletSpawner;
    Vector2 bulletSpawnerRealPos;
    private int rangedAttackTime;
    [SerializeField]
    private int maxRangedAttackTime = 4;
    //private MouseAim aim;
    float aimAngle;
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
        meleeAttackObject.SetActive(false);
        //bulletSpawnerRealPos = new Vector2(bulletSpawner.transform.position.x+0.622f, bulletSpawner.transform.position.y+0.319f);
        //aim = GetComponentInChildren<MouseAim>();
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
            flip.FlipCharacter();
            if (!isEnemyInProximity)
            {
                anim.SetBool("AttackRanged", true);
                //aimAngle = aim.CurrentAimAngleAbsolute;
                anim.SetFloat("AimAngle", aim.CurrentAimAngleAbsolute);
                
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
        int flipreload = Random.Range(0, 3);

        if (flipreload > 1)
            anim.SetBool("Reloading", true);
        else
            anim.SetBool("Flipping", true);
        if (!canRangedAttackWhileMove)
            EnableMovementAfterAttack();

        yield return new WaitForSeconds(AttackDowntime);
        anim.SetBool("Reloading", false); 
        anim.SetBool("Flipping", false);
        canAttack = true;
    }


    #region Ranged Attack Function

    //Diassign ke animation
    void SpawnBullet()
    {
        rangedAttackTime++;
        spawnProjectile(bulletSpawner.transform.position);
        anim.SetFloat("AimAngle", aim.CurrentAimAngleAbsolute);
        if (rangedAttackTime == maxRangedAttackTime)
        {
            StartCoroutine(AttackCooldown());
        }
        else if (isEnemyInProximity)
        {
            anim.SetBool("AttackRanged", false);
            anim.SetBool("AttackMelee", true);
        }
    }

    void spawnProjectile(Vector2 spawnPosition)
    {
        GameObject projectilePooled = pooler.GetObjectFromPool();
        projectilePooled.transform.position = spawnPosition;
        projectilePooled.SetActive(true);

        Projectile projectile = projectilePooled.GetComponent<Projectile>();
        Vector2 newDirection = flip.isFlipped ? -bulletSpawner.transform.right : bulletSpawner.transform.right;

        projectile.SetDirection(newDirection, bulletSpawner.transform.rotation);
    }


    void DisableMovementWhileAttack()
    {
        if (jump.isGrounded)
        {
            movement.enabled = false;
            //jump.enabled = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void EnableMovementAfterAttack()
    {
        movement.enabled = true;
        //jump.enabled = true;
    }
    #endregion

    #region Melee Attack Function

    //Diassign ke animation
    void MeleeAttack()
    {
        meleeAttackTime++;
        if (meleeAttackTime == maxMeleeAttackTime)
        {
            anim.SetTrigger("Slash 2");
            Debug.Log("Slash 2");
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
