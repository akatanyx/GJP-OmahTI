using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterFlip : Character
{
    float scaleX;
    Vector2 facingRight, facingLeft;
    float weaponAngle;
    //MouseAim aim;
    bool isPaused;

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    void Start()
    {
        scaleX = transform.localScale.x;
        facingRight = new Vector2(scaleX, transform.localScale.y);
        facingLeft = new Vector2(-scaleX, transform.localScale.y);
        aim = GetComponentInChildren<MouseAim>();
        StartCoroutine(FlipChar());
    }

    void Update()
    {
        //if (IsPointerOverUIObject())
        //    return;
        //FlipCharacter();
    }

    public void FlipCharacter()
    {
        weaponAngle = aim.CurrentAimAngleAbsolute;
        if (dash.isDashing)
            return;
        if ((weaponAngle > 90 || weaponAngle < -90))
        {
            //transform.localScale = facingLeft;
            //isFlipped = true;
            FlipLeft();
        }
        else
        {
            //transform.localScale = facingRight;
            //isFlipped = false;
            FlipRight();
        }
    }

    public void FlipLeft()
    {
        transform.localScale = facingLeft;
        isFlipped = true;
    }
    
    public void FlipRight()
    {
        transform.localScale = facingRight;
        isFlipped = false;
    }


    IEnumerator FlipChar()
    {
        while (!isPaused)
        {
            FlipCharacter();
            yield return new WaitForSeconds(.1f);
        }
    }
}
