using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : GameManager
{
    public float halfCameraX, halfCameraY;
    void Start()
    {
        Initialization();
    }
    protected override void Initialization()
    {
        base.Initialization();
        halfCameraX = GetComponent<Camera>().ViewportToWorldPoint(new Vector2(0, 0)).x;
        halfCameraY = GetComponent<Camera>().ViewportToWorldPoint(new Vector2(0, 0)).y;
    }

    void FixedUpdate()
    {
        FollowPlayer();
        
    }

    protected virtual void FollowPlayer()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin - halfCameraX, xMax + halfCameraX), Mathf.Clamp(transform.position.y, yMin - halfCameraY, yMax + halfCameraY), -10);

    }
}
