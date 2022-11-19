//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AIBullet : MonoBehaviour
//{
//    [SerializeField] float moveSpeed, lifeTime = 2;
//    Rigidbody2D rb;
//    InputManager target;
//    [SerializeField] int damageDeal = 5;
//    UIManager uiManager;
//    //Character target;
//    Vector2 moveDirection;
//    private void Awake()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        target = FindObjectOfType<InputManager>();
//    }
//    void Start()
//    {
//        uiManager = FindObjectOfType<UIManager>();
//        //rb = GetComponent<Rigidbody2D>();
//        //target = FindObjectOfType<InputManager>();
//        //moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
//        //Destroy(gameObject, 3f);
//    }
//    void FixedUpdate()
//    {
//        MoveProjectile();
//    }
//    public void MoveProjectile()
//    {
//        //moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;// * Time.fixedDeltaTime;
//        ////rb.MovePosition(rb.position + moveDirection);
//        //rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
//        //Speed += acceleration * Time.deltaTime;
//    }
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Player"))
//        {
//            //PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health") - damageDeal);
//            //uiManager.UpdateHealth();
//            Return();
//        }
//    }

//    public void Return()
//    {
//        gameObject.SetActive(false);
//    }

//    private void OnEnable()
//    {
//        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
//        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
//        Invoke(nameof(Return), lifeTime);
//    }

//    private void OnDisable()
//    {
//        CancelInvoke();
//    }

//    private void OnBecameInvisible()
//    {
//        gameObject.SetActive(false);
//        CancelInvoke();
//    }
//}
