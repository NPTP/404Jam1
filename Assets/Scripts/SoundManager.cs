using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ---------------------------
// DEPRECATED
// ---------------------------

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;
    private AudioSource audioSource;
    private AudioClip[] bounceSounds;
    private AudioClip[] smashSounds;
    private int roundRobinIndex = 0;

    public AudioSource sfxSource;

    void Start()
    {
        soundManager = this;
        bounceSounds = Resources.LoadAll<AudioClip>("bounce");
        smashSounds = Resources.LoadAll<AudioClip>("smash");
    }

    // Plays random bounce sound
    public void PlayBounceSound()
    {
        int randomIndex = Random.Range(0, bounceSounds.Length);
        sfxSource.PlayOneShot(bounceSounds[randomIndex]);
    }

    // Plays round robin smash sound
    public void PlaySmashSound()
    {
        if (roundRobinIndex == smashSounds.Length)
        {
            roundRobinIndex = 0;
        }
        sfxSource.PlayOneShot(smashSounds[roundRobinIndex]);
        roundRobinIndex++;
    }
}
