using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Created with the help of this tutorial
//https://www.youtube.com/watch?v=1QfxdUpVh5I
public class PlayerAttack : MonoBehaviour
{
    //attack timer
    public float attackTimer;
    public float startAttackTime;

    public Transform attackPos;
    public float attackRange;
    public LayerMask findEnemyPlayer;

    public static PlayerInput thisPlayer;

    public int damageAmount = 5;

    private void Awake()
    {
        thisPlayer = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer >= 0)
        {
            //attack functionality
            if (Input.GetButtonDown("Fire2"))
            {
                Debug.Log("Attacking");
                Collider2D[] enemyPlayer = Physics2D.OverlapCircleAll(attackPos.position, attackRange, findEnemyPlayer);
                for (int i = 0; i < enemyPlayer.Length; i++)
                {
                    if (thisPlayer.isHammerHeld == true)
                    {
                        enemyPlayer[i].GetComponent<Player2Input>().playerHealth -= 10;
                    }
                    else
                    {
                        enemyPlayer[i].GetComponent<Player2Input>().isHammerHeld = false; 
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
