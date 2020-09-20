using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverButton : MonoBehaviour
{
    public AudioSource audioSource;

    public void HoverSound()
    {
        audioSource.Play();
    }
}
