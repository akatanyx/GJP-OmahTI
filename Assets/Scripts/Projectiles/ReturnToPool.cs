using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    [SerializeField] float lifeTime = 2f;
    [SerializeField] int damage;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Return()
    {
        gameObject.SetActive(false);
        rb.simulated = true;
    }
    private void OnEnable()
    {
        Invoke(nameof(Return), lifeTime);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
}
