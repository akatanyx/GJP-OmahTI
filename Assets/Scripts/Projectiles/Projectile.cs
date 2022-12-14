using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 100f, acceleration;
    [SerializeField] private int damage;
    [SerializeField] string enType;

    public Vector2 Direction { get; set; }
    public float Speed { get; set; }
    private Rigidbody2D rb;
    public bool isFlying = true;
    private Vector2 movement;
    private ReturnToPool returnToPool;
    void Awake()
    {
        Speed = speed;
        rb = GetComponent<Rigidbody2D>();
        returnToPool = GetComponent<ReturnToPool>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isFlying)
        {
            MoveProjectile();
        }
    }
    public void MoveProjectile()
    {
        movement = Direction * speed * Time.fixedDeltaTime;
        //rb.MovePosition(rb.position + movement);
        rb.velocity = movement;
        Speed += acceleration * Time.deltaTime;
    }

    public void SetDirection(Vector2 newDirection, Quaternion rotation)
    {
        Direction = newDirection;
        transform.rotation = rotation;
    }

    private void OnBecameInvisible()
    {
        returnToPool.Return();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(enType))
        {
            //Debug.Log("Hit " + enType);
            returnToPool.Return();
            collision.GetComponent<HealthManager>().DealDamage(damage, tag);
        }
    }
}
