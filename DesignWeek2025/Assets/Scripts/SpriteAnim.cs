using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnim : MonoBehaviour

{

    public Sprite sprite1;

    public Sprite sprite2;

    public PlayerInput input;

    private SpriteRenderer spriteRenderer;

    private bool isSprite1Active = true;

    private float switchInterval = 0.50f; // Time between sprite switches



    void Start()

    {

        spriteRenderer = GetComponent<SpriteRenderer>();

    }



    void Update()

    {

       input.moveInput.x = Input.GetAxisRaw("Horizontal");

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

}
