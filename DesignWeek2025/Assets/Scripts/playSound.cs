using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSound : MonoBehaviour
{
    public AudioSource jump;
    public AudioSource kick;

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

        if (Input.GetButtonDown("Fire2"))
        {
            PlayKick();
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
}
