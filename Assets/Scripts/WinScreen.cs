using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public AudioSource audioSrc;

    public void PlayAgain()
    {
        audioSrc.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    // Unused
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
