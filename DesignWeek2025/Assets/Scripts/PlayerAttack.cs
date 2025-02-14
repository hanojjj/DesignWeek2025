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

    public float knockbackStrength;
    public static Rigidbody2D enemyRB;

    private void Awake()
    {
        thisPlayer = GetComponent<PlayerInput>();
    }

    //sprite renderer
    private SpriteRenderer spriteRenderer;
    public Sprite attackHammerSprite1;
    public Sprite attackHammerSprite2;
    public Sprite attackSprite1;
    public Sprite attackSprite2;
    public Sprite originalSprite;
    private bool isSprite1Active = true;
    private float switchInterval = 0.50f;
    public bool isAttacking;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Initialize the SpriteRenderer
        spriteRenderer.sprite = originalSprite; // Set the original sprite initially
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer >= 0)
        {
            //attack functionality
            if (Input.GetButtonDown("Fire2") && !isAttacking)
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
                        //calculate knockback
                        enemyRB = enemyPlayer[i].GetComponent<Rigidbody2D>();
                        Vector2 direction = (enemyPlayer[i].gameObject.transform.position - transform.position).normalized;
                        Vector2 knockback = direction * knockbackStrength;
                        enemyRB.AddForce(knockback, ForceMode2D.Impulse);
                    }
                    Debug.Log("Attacking");
                }

                
            }

            if (Input.GetButtonDown("Fire2") && thisPlayer.isHammerHeld == true)
            {
                StartCoroutine(AttackWithHammerP1());
            }
            else if (Input.GetButtonDown("Fire2") && thisPlayer.isHammerHeld == false)
            {
                StartCoroutine(AttackWithoutHammerP1());
            }


            attackTimer = startAttackTime;
        }
        else
        {
            //countdown to next attack
            attackTimer -= Time.deltaTime;
        }


    }

    private IEnumerator AttackWithHammerP1()
    {
        isAttacking = true;

        // Change to the first attack sprite
        spriteRenderer.sprite = attackHammerSprite1;
        yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds

        // Change to the second attack sprite
        spriteRenderer.sprite = attackHammerSprite2;
        yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds

        // Change back to the original sprite
        spriteRenderer.sprite = originalSprite;

        isAttacking = false;
    }

    private IEnumerator AttackWithoutHammerP1()
    {
        isAttacking = true;

        // Change to the first attack sprite
        spriteRenderer.sprite = attackSprite1;
        yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds

        // Change to the second attack sprite
        spriteRenderer.sprite = attackSprite2;
        yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds

        // Change back to the original sprite
        spriteRenderer.sprite = originalSprite;

        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
