using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRangedAttack : AIManagers
{
    [SerializeField] GameObject bullet;
    //[HideInInspector]
    [SerializeField]float fireRate;
    [SerializeField]float bulletSpeed;
    [HideInInspector]public float nextFire;
    public ObjectPooling Pooler { get; set; }
    void Start()
    {
        Pooler = GetComponent<ObjectPooling>();
        fireRate = 1f;
        nextFire = 0;
        timeTillDoAction = 0;
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfTimeToFire();
    }

    void CheckIfTimeToFire()
    {
        if (nextFire <= 0 && enemyCharacter.playerIsClose && GetComponent<HealthManager>().healthPoints > 0)
        {
            //Instantiate(bullet, transform.position, Quaternion.identity);
            GameObject projectilePooled = Pooler.GetObjectFromPool();
            projectilePooled.transform.position = transform.position;

            Projectile projectile = projectilePooled.GetComponent<Projectile>();
            //Vector2 newDirection = flip.isFlipped ? -bulletSpawner.transform.right : bulletSpawner.transform.right;

            Vector2 moveDirection = (player.transform.position-transform.position).normalized;
            projectile.SetDirection(moveDirection, transform.rotation);

            //projectile.GetComponent<Rigidbody2D>().velocity = moveDirection;
            projectilePooled.SetActive(true);
            nextFire = fireRate;
        }
        nextFire -= Time.deltaTime;
    }
}


