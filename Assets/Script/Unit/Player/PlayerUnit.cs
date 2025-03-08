using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerUnit : Unit
{
    private static PlayerUnit _instance;
    public static PlayerUnit Get() => _instance;

    public PlayerUnit()
    {
        _instance = this;
    }

    SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2D;
    private new Collider2D Collider2D;
    private bool isJump = true;
    private float jumpPower = 5f;

    private bool isAttack = true;
    [SerializeField]
    private GameObject wepon;

    void Start()
    {
        Init();
    }

    void Update()
    {
        Move();
        Jump();
        StartCoroutine(Attack());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Ground"))
        {
            isJump = true;
        }
    }

    public override IEnumerator Attack()
    {
        if(isAttack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("АјАн");
                wepon.GetComponent<BoxCollider2D>().enabled = true;
                isAttack = false;
                yield return new  WaitForSeconds(attackCoolTime);
                WeponColliderOnOff();

                isAttack = true;
            }
        }
      
    }
    

    public override void Move()
    {
        if (Input.GetButton("Horizontal"))
        {
            float h = Input.GetAxisRaw("Horizontal");
            Vector3 dir = new Vector3(h, 0f, 0f).normalized;
            transform.Translate(dir * moveSpeed * Time.deltaTime);
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            wepon.transform.localPosition = new Vector3(h * 0.5f, wepon.transform.localPosition.y, 0f);
        }
    }

    public void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isJump)
            {
                isJump = false;
                rigidbody2D.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
            }
        }
    }

    public void Init()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        this.attackCoolTime = 0.2f;
        this.damage = 1;
    }

    public void WeponColliderOnOff()
    {
        if(!isAttack)
        {
            wepon.GetComponent<BoxCollider2D>().enabled = false;

        }
    }
}
