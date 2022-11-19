using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : Character
{
    [SerializeField] float maxSpeed;
    float currentSpeed;
    [HideInInspector]
    public float horizontalInput;

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private bool MovementPressed()
    {
        if (inputManager.RightHeld())
        {
            horizontalInput = 1;
            //flip.FlipCharacter();
            return true;
        }
        else if (inputManager.LeftHeld())
        {
            horizontalInput = -1;
            //flip.FlipCharacter();
            return true;
        }
        else
            return false;
    }

    private void MoveCharacter()
    {
        if (MovementPressed())
        {
            currentSpeed = horizontalInput * maxSpeed;
            anim.SetBool("Moving", true);
        }
        else
        {
            currentSpeed = 0;
            anim.SetBool("Moving", false);
        }
        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
    }

}
