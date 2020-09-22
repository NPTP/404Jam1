using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreUIScript : MonoBehaviour
{
    public int numDestroyed = 0;
    public bool hitSomething = false;
    public int destroyGoal = 10;
    public Text scoreNumber;

    void Update()
    {
        if (hitSomething)
        {
            numDestroyed++;
            hitSomething = false;
        }

        scoreNumber.text = (numDestroyed * 100).ToString("0");

        if (numDestroyed == destroyGoal)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
