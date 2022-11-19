using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : Character
{
    [SerializeField] float jumpForce, distanceToCollider;
    public LayerMask collisionLayer;
    //private bool isJumping;

    private void Update()
    {
        Jump();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        GroundCheck();
    }

    private void Jump()
    {
        if (inputManager.JumpPressed() && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
            //anim.SetBool("IsJumping", true);
        }
    }

    //private bool IsJumping()
    //{
    //    if (rb.velocity.y == 0)
    //    {
    //        anim.SetBool("IsJumping", false);
    //        return false;
    //    }
    //    else
    //    {
    //        anim.SetBool("IsJumping", true);
    //        return true;
    //    }
    //}
    protected virtual void GroundCheck()
    {
        if (CollisionCheck(Vector2.down, distanceToCollider, collisionLayer) && rb.velocity.y == 0)// && !IsJumping())
        {
            isGrounded = true;
            anim.SetBool("IsGrounded", true);
        }
        else
        {
            isGrounded = false;
            anim.SetBool("IsGrounded", false);
        }
    }
}
