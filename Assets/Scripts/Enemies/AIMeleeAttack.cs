using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMeleeAttack : AIManagers
{
    //This bool will make sure the Enemy doesn't swing the Melee weapon if the player isn't close to the Enemy
    [SerializeField]
    protected bool hitPlayerWhenClose;
    //How much damage needs to be applied to the Player when the Player is hit by the attack
    [SerializeField]
    protected int damageAmount; 

    //A collider that gets adjusted through an animation to determine if the Player is inside that collider
    [SerializeField] protected Collider2D swipeCollider;
    //The animation that needs to play when attacking
    [SerializeField] protected Animator anim;
    //The game object that is the physical attack; this game object appears as the slashing sprites from the animation
    [SerializeField]protected GameObject swipe;
    //A quick reference to the PlayerHealth script to deal damage
    //protected PlayerHealth playerHealth;
    //A quick bool that turns true if the melee attack struck the Player
    protected bool hit;

    [SerializeField]
    protected bool canKnockback;
    [SerializeField]
    protected int knockbackForce;
    [SerializeField]
    protected float knockbackTime;
    void Start()
    {
        Initialization();
    }
    protected override void Initialization()
    {
        base.Initialization();
        swipe = transform.GetChild(0).gameObject;
        anim = GetComponent<Animator>();
        swipeCollider = swipe.GetComponent<Collider2D>();
        //playerHealth = player.GetComponent<PlayerHealth>();
        swipe.SetActive(false);
    }

    protected virtual void FixedUpdate()
    {
        HitPlayer();
    }

    //If the Player is inside the trigger collider of the swipe, then it sets the hit bool to true, and runs the DealDamage method
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !hit)
        {
            hit = true;
            collision.GetComponent<HealthManager>().DealDamage(damageAmount, tag, canKnockback, knockbackForce, -transform.localScale.x);
            StartCoroutine(DisableMovementAfterAttack());
        }
    }

  
    //This method manages if and when the Enemy should attack the Player; most of this logic is managed by whether or not the Enemy should attack only when close, and how much time is left in the timeTillDoAction variable
    protected virtual void HitPlayer()
    {
        if (hitPlayerWhenClose && !enemyCharacter.playerIsClose)
        {
            return;
        }
        timeTillDoAction -= Time.deltaTime;
        //swipe.SetActive(false);
        if (timeTillDoAction <= 0)
        {
            
            //GetComponent<Animator>().SetTrigger("Attack");
            //anim.SetBool("Attack", true);
            anim.SetTrigger("Attack");
            timeTillDoAction = originalTimeTillDoAction;
            if (hit)
            {
                hit = false;
            }
        }
        else
        {
            //CancelSwipe();
        }
        //Invoke("CancelSwipe", anim.GetCurrentAnimatorStateInfo(0).length);
    }

    IEnumerator DisableMovementAfterAttack()
    {
        float initialSpeed = enemyMovement.maxSpeed;
        enemyMovement.maxSpeed = 0;
        yield return new WaitForSeconds(originalTimeTillDoAction);
        enemyMovement.maxSpeed = initialSpeed;
    }

    //Manages the animation and disables the swipe game object from the scene until the Enemy melee attacks again
    protected virtual void CancelSwipe()
    {
        //anim.SetBool("Attack", false);
        swipe.SetActive(false);
    }

    public void ShowHitCollider()
    {
        swipe.SetActive(true);
    }
    public void UnShowHitCollider()
    {
        swipe.SetActive(false);
    }
}
