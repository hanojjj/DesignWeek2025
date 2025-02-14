using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSoundP2 : MonoBehaviour
{

    public Player2Input thisPlayerP2;


    public AudioSource jumpP2;
    public AudioSource kickP2;
    public AudioSource hammerP2;


    void Start()
    {

    }

    void Update()
    {
        // Check if the "Fire1" button is pressed
        if (Input.GetButtonDown("P2 Jump"))
        {
            PlayJumpP2();
        }

        if (Input.GetButtonDown("P2 Fire2") && thisPlayerP2.isHammerHeld == false)
        {
            PlayKickP2();
        }

        if (Input.GetButtonDown("P2 Fire2") && thisPlayerP2.isHammerHeld == true)
        {
            PlayHammerP2();
        }

    }

    void PlayJumpP2()
    {
        // Play the audio clip assigned to the AudioSource
        if (jumpP2 != null && !jumpP2.isPlaying)
        {
            jumpP2.Play();
        }
    }

    void PlayKickP2()
    {
        // Play the audio clip assigned to the AudioSource
        if (kickP2 != null)
        {
            kickP2.Play();
        }
    }

    void PlayHammerP2()
    {
        // Play the audio clip assigned to the AudioSource
        if (hammerP2 != null)
        {
            hammerP2.Play();
        }
    }
}
