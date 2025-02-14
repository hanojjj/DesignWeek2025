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

    private SpriteRenderer spriteRenderer;
    public Sprite attackHammerSprite3;
    public Sprite attackHammerSprite4;
    public Sprite attackSprite3;
    public Sprite attackSprite4;
    public Sprite originalSprite2;
    private bool isSprite1Active = true;
    private float switchInterval = 0.50f;
    public bool isAttacking;

    private void Awake()
    {
        thisPlayer = GetComponent<Player2Input>();
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Initialize the SpriteRenderer
        spriteRenderer.sprite = originalSprite2;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer >= 0)
        {
            //attack functionality
            if (Input.GetButtonDown("P2 Fire2") && !isAttacking)
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

            if (Input.GetButtonDown("P2 Fire2") && thisPlayer.isHammerHeld == true)
            {
                StartCoroutine(AttackWithHammerP2());
            }
            else if (Input.GetButtonDown("P2 Fire2") && thisPlayer.isHammerHeld == false)
            {
                StartCoroutine(AttackWithoutHammerP2());
            }

            attackTimer = startAttackTime;
        }
        else
        {
            //countdown to next attack
            attackTimer -= Time.deltaTime;
        }


    }

    private IEnumerator AttackWithHammerP2()
    {
        isAttacking = true;

        // Change to the first attack sprite
        spriteRenderer.sprite = attackHammerSprite3;
        yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds

        // Change to the second attack sprite
        spriteRenderer.sprite = attackHammerSprite4;
        yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds

        // Change back to the original sprite
        spriteRenderer.sprite = originalSprite2;

        isAttacking = false;
    }

    private IEnumerator AttackWithoutHammerP2()
    {
        isAttacking = true;

        // Change to the first attack sprite
        spriteRenderer.sprite = attackSprite3;
        yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds

        // Change to the second attack sprite
        spriteRenderer.sprite = attackSprite4;
        yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds

        // Change back to the original sprite
        spriteRenderer.sprite = originalSprite2;

        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
