using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region Character State
    [HideInInspector]
    public bool isGrounded;
    [HideInInspector]
    public bool isFlipped;
    [HideInInspector]
    public bool isDashing;

    #endregion

    //List Komponen
    #region Komponen
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected Animator anim;
    protected SpriteRenderer sprite;

    protected CharacterMovement movement;
    protected CharacterJump jump;
    protected CharacterInputManager inputManager;
    protected CharacterFlip flip;
    protected CharacterAttack attack;
    protected CharacterDash dash;
    protected MouseAim aim;
    #endregion

    public static Character instance;
    private void Awake()
    {
        instance = this;
        Initialization();
    }

    protected virtual void Initialization()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        movement = GetComponent<CharacterMovement>();
        jump = GetComponent<CharacterJump>();
        flip = GetComponent<CharacterFlip>();
        attack = GetComponent<CharacterAttack>();
        dash = GetComponent<CharacterDash>();
        inputManager = GetComponent<CharacterInputManager>();
        aim = GetComponentInChildren<MouseAim>();
    }

    protected virtual bool CollisionCheck(Vector2 direction, float distance, LayerMask collision)
    {
        RaycastHit2D[] hits = new RaycastHit2D[10];
        int numHits = col.Cast(direction, hits, distance);
        for (int i = 0; i < numHits; i++)
        {
            if ((1 << hits[i].collider.gameObject.layer & collision) != 0)
            {
                return true;
            }
        }
        return false;
    }
}
