using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public AudioSource audioSource;

    public void StartGame()
    {
        audioSource.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Unused
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
