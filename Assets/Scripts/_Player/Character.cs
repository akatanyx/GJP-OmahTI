using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region Character State
    [HideInInspector]
    public bool isGrounded;

    #endregion

    //List Komponen
    #region Komponen
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected CharacterMovement movement;
    protected CharacterJump jump;
    protected CharacterInputManager inputManager;

    #endregion

    private void Awake()
    {
        Initialization();
    }

    protected virtual void Initialization()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        inputManager = GetComponent<CharacterInputManager>();
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
