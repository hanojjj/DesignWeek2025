using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSound : MonoBehaviour
{

    public PlayerInput thisPlayer;


    public AudioSource jump;
    public AudioSource kick;
    public AudioSource hammer;


    void Start()
    {
        
    }

    void Update()
    {
        // Check if the "Fire1" button is pressed
        if (Input.GetButtonDown("Jump"))
        {
            PlayJump();
        }

        if (Input.GetButtonDown("Fire2") && thisPlayer.isHammerHeld == false)
        {
            PlayKick();
        }

        if (Input.GetButtonDown("Fire2") && thisPlayer.isHammerHeld == true)
        {
            PlayHammer();
        }

    }

    void PlayJump()
    {
        // Play the audio clip assigned to the AudioSource
        if (jump != null && !jump.isPlaying)
        {
            jump.Play();
        }
    }

    void PlayKick()
    {
        // Play the audio clip assigned to the AudioSource
        if (kick != null)
        {
            kick.Play();
        }
    }

    void PlayHammer()
    {
        // Play the audio clip assigned to the AudioSource
        if (hammer != null)
        {
            hammer.Play();
        }
    }
}
