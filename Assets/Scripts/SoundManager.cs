using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;
    private AudioSource audioSource;
    private AudioClip[] bounceSounds;
    private AudioClip[] smashSounds;

    void Start()
    {
        soundManager = this;
        audioSource = GetComponent<AudioSource>();
        bounceSounds = Resources.LoadAll<AudioClip>("bounce");
        smashSounds = Resources.LoadAll<AudioClip>("smash");
    }

    public void PlayBounceSound()
    {
        int randomIndex = Random.Range(0, bounceSounds.Length);
        audioSource.PlayOneShot(bounceSounds[randomIndex]);
    }

    public void PlaySmashSound()
    {
        int randomIndex = Random.Range(0, smashSounds.Length);
        audioSource.PlayOneShot(smashSounds[randomIndex]);
    }
}
