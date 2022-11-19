using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFlip : Character
{
    float scaleX;
    Vector2 facingRight, facingLeft;
    void Start()
    {
        scaleX = transform.localScale.x;
        facingRight = new Vector2(scaleX, transform.localScale.y);
        facingLeft = new Vector2(-scaleX, transform.localScale.y);
    }


    public void FlipCharacter()
    {
        if(movement.horizontalInput > 0)
        {
            transform.localScale = facingRight;
            isFlipped = false;
        }
        else
        {
            transform.localScale = facingLeft;
            isFlipped = true;
        }
    }
}
