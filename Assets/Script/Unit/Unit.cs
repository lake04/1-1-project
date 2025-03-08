using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float maxHp;
    public float currentHp;
    public float moveSpeed;
    public float damage;
    private bool isAttack;
    public float attackCoolTime;

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }

    public virtual void Init()
    {
        currentHp = maxHp;
    }

    public virtual IEnumerator Attack()
    {
        yield return null;
    }

    public virtual void Move()
    {
        
    }
    public virtual void TakeDamage(float damage)
    {
        if(currentHp>=0)
        {
            currentHp-= damage;
             if (currentHp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        else if(currentHp<=0)
        {
            Destroy(this.gameObject);
        }
    }
}
