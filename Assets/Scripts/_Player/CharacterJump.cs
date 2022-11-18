using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : Character
{
    [SerializeField] float jumpForce, distanceToCollider;
    public LayerMask collisionLayer;
    private bool isJumping;

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
        }
    }

    protected virtual void GroundCheck()
    {
        if (CollisionCheck(Vector2.down, distanceToCollider, collisionLayer) && !isJumping)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
