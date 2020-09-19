using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreUI : MonoBehaviour
{
    public int numDestroyed;
    public int destroyGoal = 10;
    public Text scoreNumber;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            numDestroyed++;
        }

        scoreNumber.text = (numDestroyed * 100).ToString("0");
        if (numDestroyed == destroyGoal)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
