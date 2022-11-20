using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDash : Character
{
    private bool canDash = true;
    [SerializeField]
    private float dashingPower = 25;
    [SerializeField]
    private float dashingTime;
    [SerializeField]
    private float dashingCooldown;
    // Update is called once per frame
    void Update()
    {
        if (inputManager.DashPressed() && canDash && (inputManager.LeftHeld() || inputManager.RightHeld()))
        {
            StartCoroutine(TriggerDash());
            StartCoroutine(IFrames());
        }
    }

    private IEnumerator TriggerDash()
    {
        canDash = false;
        isDashing = true;
        anim.SetBool("Dashing", true);
        anim.SetBool("AttackRanged", false);

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        if (inputManager.LeftHeld())
        {
            rb.velocity = new Vector2(-1 * dashingPower, 0f);
            flip.FlipLeft();
        }
        else
        {
            rb.velocity = new Vector2(1 * dashingPower, 0f);
            flip.FlipRight();
        }
        //if (flip.isFlipped)
        //    rb.velocity = new Vector2(-1 * dashingPower, 0f);
        //else
        //    rb.velocity = new Vector2(1 * dashingPower, 0f);
        movement.enabled = false;
        jump.enabled = false;
        attack.enabled = false;

        yield return new WaitForSeconds(dashingTime);

        isDashing = false;
        anim.SetBool("Dashing", false);
        rb.gravityScale = originalGravity;

        movement.enabled = true;
        jump.enabled = true;
        attack.enabled = true;

        yield return new WaitForSeconds(dashingCooldown);

        canDash = true;
    }

    IEnumerator IFrames()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        Physics2D.IgnoreLayerCollision(7, 9, true);
        sprite.color = new Color(1, 1, 1, .5f);
        yield return new WaitForSeconds(dashingTime);
        sprite.color = Color.white;
        Physics2D.IgnoreLayerCollision(7, 8, false);
        Physics2D.IgnoreLayerCollision(7, 9, false);
    }
}
