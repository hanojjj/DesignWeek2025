using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //Declarations
    public Rigidbody2D playerRB { get; private set; }
    //variables for movement
    public float moveSpeed = 5;
    public float acceleration = 1;
    public float deceleration = 1;
    public float velocityPower = 2;
    public float frictionAmount = 0.2f;
    public Vector2 moveInput;
    //Jumping
    public float jumpHeight = 10;
    public float variableJumpHeight = 0.5f;
    public float fallGravityMultiplier = 2.0f;
    float gravityScale = 1.0f;
    //Jump check raycast
    public float castDistance;
    public Vector2 boxSize;
    public LayerMask groundLayer;
    //Coyote Time
    public float coyoteTimer = 0.2f;
    private float coyoteTimeCounter;
    //Jump Buffer
    public float jumpBufferTimer = 0.2f;
    private float jumpBufferCounter;
    //Sprite Renderer & Switcher
    private SpriteRenderer spriteRenderer;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite hsprite1;
    //public Sprite hsprite2;
    private bool isSprite1Active = true;
    private float switchInterval = 0.50f;
    //Interacting
    bool canInteract;
    public static GameObject hammer;
    public bool isHammerHeld;
    //Health
    public int playerHealth = 10;

    public static Player2Input p2;
    public PlayerAttack p1Attack;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hammer = GameObject.FindGameObjectWithTag("Hammer");
        p2 = GameObject.FindObjectOfType<Player2Input>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (isHammerHeld == true)
        {
            spriteRenderer.sprite = hsprite1;
        }*/

        //interaction
        if (Input.GetButtonDown("Fire1") && canInteract)
        {
            isHammerHeld = true;
            canInteract = false;
            hammer.SetActive(false);
            Debug.Log("Interacting");
        }
        if (isHammerHeld == false && p2.isHammerHeld == false)
        {
            hammer.SetActive(true);
        }
        #region Run Variables
        //calculate the desired top speed
        float topSpeed = moveInput.x * moveSpeed;
        //find difference between top speed and current speed
        float speedDif = topSpeed - playerRB.velocity.x;
        //change acceleration based on situation
        float accelRate = (Mathf.Abs(topSpeed) > 0.01f) ? acceleration : deceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velocityPower) * Mathf.Sign(speedDif);
        #endregion
        #region Friction
        if (Mathf.Abs(moveInput.x) < 0.01f)
        {
            //friction logic, returns either the velocity or the friction
            float amount = Mathf.Min(Mathf.Abs(playerRB.velocity.x), Mathf.Abs(frictionAmount));
            //applies to movement direction
            amount *= Mathf.Sign(playerRB.velocity.x);
            //applies force against movement direction
            playerRB.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
        #endregion
        //Get inputs
        moveInput.x = Input.GetAxisRaw("Horizontal");
        //move character horizontally
        playerRB.AddForce(movement * Vector2.right);

        /*if (Input.GetAxisRaw("Horizontal") < 0 && isHammerHeld == false)
        {
            spriteRenderer.flipX = spriteRenderer.flipX;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && isHammerHeld == true)
        {
            spriteRenderer.sprite = hsprite1;
            spriteRenderer.flipX = spriteRenderer.flipX;
            transform.localScale = new Vector3(-1, 1, 1);
        }*/


        if (Input.GetAxisRaw("Horizontal") > 0 && isHammerHeld == false)
        {
            spriteRenderer.flipX = spriteRenderer.flipX;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && isHammerHeld == false)
        {
            spriteRenderer.flipX = spriteRenderer.flipX;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        /*else if (Input.GetAxisRaw("Horizontal") < 0 && isHammerHeld == true)
        {
            spriteRenderer.sprite = hsprite1;
            spriteRenderer.flipX = spriteRenderer.flipX;
            transform.localScale = new Vector3(-1, 1, 1);
        }*/
        else if (Input.GetAxisRaw("Horizontal") < 0 && isHammerHeld == true && !p1Attack.isAttacking)
        {
            spriteRenderer.sprite = hsprite1;
            spriteRenderer.flipX = spriteRenderer.flipX;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        /*else if (Input.GetAxisRaw("Horizontal") > 0 && isHammerHeld == true)
        {
            spriteRenderer.sprite = hsprite1;
            spriteRenderer.flipX = spriteRenderer.flipX;
            transform.localScale = new Vector3(1, 1, 1);
        }*/
        else if (Input.GetAxisRaw("Horizontal") > 0 && isHammerHeld == true && !p1Attack.isAttacking)
        {
            spriteRenderer.sprite = hsprite1;
            spriteRenderer.flipX = spriteRenderer.flipX;
            transform.localScale = new Vector3(1, 1, 1);
        }

            //flip sprite
            if (playerRB.velocity.x < 0f && isHammerHeld == false)
        {
            if (Time.time > switchInterval)
            {
                
                isSprite1Active = !isSprite1Active;

                if (isSprite1Active)
                {
                    spriteRenderer.sprite = sprite1;
                }
                else
                {
                    spriteRenderer.sprite = sprite2;
                }

                switchInterval += 0.50f; // Reset the switch timer
            }
        }
        if (playerRB.velocity.x > 0f && isHammerHeld == false)
        {

            if (Time.time > switchInterval)
            {
                
                isSprite1Active = !isSprite1Active;

                if (isSprite1Active)
                {
                    spriteRenderer.sprite = sprite1;
                }
                else
                {
                    spriteRenderer.sprite = sprite2;
                }

                switchInterval += 0.50f; // Reset the switch timer
            }
        }
        //Jump
        if (isGrounded())
        {
            coyoteTimeCounter = coyoteTimer;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (UnityEngine.Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTimer;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            //playerRB.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpHeight);
            jumpBufferCounter = 0f;
        }
        if (UnityEngine.Input.GetButtonUp("Jump") && playerRB.velocity.y > 0f)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y * (1 - variableJumpHeight));
            coyoteTimeCounter = 0f;
            
        }

        //fall gravity
        if (playerRB.velocity.y < 0)
        {
            playerRB.gravityScale = gravityScale * fallGravityMultiplier;
        }
        else
        {
            playerRB.gravityScale = gravityScale;
        }

        //Death

        if (playerHealth <= 0)
        {
            //Death logic goes here
            Debug.Log("Player Died");
        }
    }

    public bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hammer")
        {
            canInteract = true;
        }
    }

}
