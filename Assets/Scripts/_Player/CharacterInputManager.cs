using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputManager : MonoBehaviour
{
    [SerializeField]
    protected KeyCode jumpKey;
    [SerializeField]
    protected KeyCode jumpKeyAlternative;
    [SerializeField]
    protected KeyCode leftKey;
    [SerializeField]
    protected KeyCode leftKeyAlternative;
    [SerializeField]
    protected KeyCode rightKey;
    [SerializeField]
    protected KeyCode rightKeyAlternative;
    [SerializeField]
    protected KeyCode dashKey;
    public bool LeftHeld()
    {
        if (Input.GetKey(leftKey) || Input.GetKey(leftKeyAlternative))
            return true;
        else
            return false;
    }
    public bool RightHeld()
    {
        if (Input.GetKey(rightKey) || Input.GetKey(rightKeyAlternative))
            return true;
        else
            return false;
    }

    public bool JumpPressed()
    {
        if (Input.GetKeyDown(jumpKey) || Input.GetKeyDown(jumpKeyAlternative))
            return true;
        else
            return false;
    }

    public bool DashPressed()
    {
        if (Input.GetKeyDown(dashKey))
            return true;
        else
            return false;
    }
}
