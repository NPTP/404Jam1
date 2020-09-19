using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceSoundTest : MonoBehaviour
{

    void Start()
    {
    }

    void OnCollisionEnter()  //Plays Sound Whenever collision detected
    {
        SoundManager.soundManager.PlayBounceSound();
    }

}
