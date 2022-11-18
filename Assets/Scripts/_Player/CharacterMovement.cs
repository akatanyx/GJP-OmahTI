using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : Character
{
    [SerializeField] float maxSpeed;
    float currentSpeed, horizontalInput;

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private bool MovementPressed()
    {
        if (inputManager.RightHeld())
        {
            horizontalInput = 1;
            return true;
        }
        else if (inputManager.LeftHeld())
        {
            horizontalInput = -1;
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
        }
        else
        {
            currentSpeed = 0;
        }
        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
    }

}
