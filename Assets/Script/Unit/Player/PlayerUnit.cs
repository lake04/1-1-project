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
    public Ghost ghost;
    private bool isDashing = false;
    private bool isCanDash = true;
    private float dashTime;
    private float maxDashTime = 1;
    private Vector2 tmpDir;

    private bool isAttack = true;
    [SerializeField]
    private GameObject wepon;
    private int comboCount =0;

    void Start()
    {
        Init();
    }

    void Update()
    {
        MoveCoterllor();
        StartCoroutine(Attack());
        ComboAttack();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isJump = true;
        }
    }

    #region 공격
    public override IEnumerator Attack()
    {
        if (isAttack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("공격");
                comboCount++;
                isAttack = false;
                yield return new WaitForSeconds(attackCoolTime);
                WeponColliderOnOff();

                isAttack = true;
            }
            else comboCount = 0;
        }
    }
    private void ComboAttack()
    {
        switch(comboCount)
        {
            case 0: spriteRenderer.color = Color.white;
                wepon.GetComponent<BoxCollider2D>().enabled = true;
                break;
            case 1: spriteRenderer.color = Color.green;
                
                break;
            case 2: spriteRenderer.color = Color.yellow;
                wepon.GetComponent<BoxCollider2D>().enabled = true; break;
            case 3: comboCount = 0;
                wepon.GetComponent<BoxCollider2D>().enabled = true; break;
        }
    }
    #endregion 
    #region 이동관련
    public void MoveCoterllor()
    {
        Move();
        Jump();
        if (Input.GetKeyDown(KeyCode.LeftShift) && isCanDash)
        {
           StartCoroutine(Dash());
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
        if (!isDashing)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (input != Vector2.zero)
            {
                tmpDir = input.normalized;
            }
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isJump)
            {
                isJump = false;
                rigidbody2D.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
            }
        }
    }

    private IEnumerator Dash()
    {
        if (!isCanDash) yield break;
        isCanDash = false;
        isDashing = true;
        ghost.makeGhost = true;

        Vector2 dashDir= tmpDir;
        if (dashDir == Vector2.zero) dashDir = Vector2.right;

        rigidbody2D.velocity = dashDir.normalized * 5;
        yield return new WaitForSeconds(maxDashTime); 

        isDashing = false;
        rigidbody2D.velocity = Vector2.zero; 
        ghost.makeGhost = false;

        yield return new WaitForSeconds(0.5f);

        isCanDash = true;
    }

    #endregion
    public void Init()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        this.attackCoolTime = 0.2f;
        this.damage = 1;
    }

    public void WeponColliderOnOff()
    {
        if (!isAttack)
        {
            wepon.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
