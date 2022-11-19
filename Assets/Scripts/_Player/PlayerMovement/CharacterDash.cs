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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.DashPressed() && canDash)
        {
            StartCoroutine(TriggerDash());
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
        rb.velocity = new Vector2(movement.horizontalInput * dashingPower, 0f);

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
}
