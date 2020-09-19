using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;
    private AudioSource audioSource;
    private AudioClip[] bounceSounds;
    private AudioClip[] smashSounds;
    private int roundRobinIndex = 0;

    void Start()
    {
        soundManager = this;
        audioSource = GetComponent<AudioSource>();
        bounceSounds = Resources.LoadAll<AudioClip>("bounce");
        smashSounds = Resources.LoadAll<AudioClip>("smash");
    }

    // Plays random bounce sound
    public void PlayBounceSound()
    {
        int randomIndex = Random.Range(0, bounceSounds.Length);
        audioSource.PlayOneShot(bounceSounds[randomIndex]);
    }

    // Plays round robin smash sound
    public void PlaySmashSound()
    {
        if (roundRobinIndex == smashSounds.Length)
        {
            roundRobinIndex = 0;
        }
        audioSource.PlayOneShot(smashSounds[roundRobinIndex]);
        roundRobinIndex++;
    }
}
