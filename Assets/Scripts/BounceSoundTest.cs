using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceSoundTest : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.soundManager.PlaySmashSound();
        }
    }

    void OnCollisionEnter()  //Plays Sound Whenever collision detected
    {
        SoundManager.soundManager.PlayBounceSound();
    }

}
