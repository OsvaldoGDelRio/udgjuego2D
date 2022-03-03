using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagment : MonoBehaviour
{
    bool GameOver = false;
    public float restartDelay = 1f;
    public GameObject completeLevelUI;
    public JohnMovement John;

    // Update is called once per frame

    public void GameEnd()
    {
        if (GameOver == false)
        {
            GameOver = true;
            Invoke("Restart", restartDelay);
        }
    }

    public void LevelComplete()
    {
        John.enabled = false;
        completeLevelUI.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
