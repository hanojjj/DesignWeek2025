using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class Player2Attack : MonoBehaviour
{
    //attack timer
    public float attackTimer;
    public float startAttackTime;

    public Transform attackPos;
    public float attackRange;
    public LayerMask findEnemyPlayer;

    public static Player2Input thisPlayer;

    public int damageAmount = 5;

    //knockback
    public float knockbackStrength;
    public static Rigidbody2D enemyRB;

    private void Awake()
    {
        thisPlayer = GetComponent<Player2Input>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer >= 0)
        {
            //attack functionality
            if (Input.GetButtonDown("P2 Fire2"))
            {
                Debug.Log("Attacking");
                Collider2D[] enemyPlayer = Physics2D.OverlapCircleAll(attackPos.position, attackRange, findEnemyPlayer);
                for (int i = 0; i < enemyPlayer.Length; i++)
                {
                    if (thisPlayer.isHammerHeld == true)
                    {
                        enemyPlayer[i].GetComponent<PlayerInput>().playerHealth -= damageAmount;
                    }
                    else
                    {
                        
                        enemyPlayer[i].GetComponent<PlayerInput>().isHammerHeld = false;
                        //calculate knockback
                        enemyRB = enemyPlayer[i].GetComponent<Rigidbody2D>();
                        Vector2 direction = (enemyPlayer[i].gameObject.transform.position - transform.position).normalized;
                        Vector2 knockback = direction * knockbackStrength;
                        enemyRB.AddForce(knockback, ForceMode2D.Impulse);
                    }
                    Debug.Log("Attacking");
                }
            }
            attackTimer = startAttackTime;
        }
        else
        {
            //countdown to next attack
            attackTimer -= Time.deltaTime;
        }


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
