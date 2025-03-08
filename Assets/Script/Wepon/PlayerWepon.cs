using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWepon : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyUnit>().TakeDamage(PlayerUnit.Get().damage);
            Debug.Log($"{PlayerUnit.Get().damage}");
        }
    }
}
