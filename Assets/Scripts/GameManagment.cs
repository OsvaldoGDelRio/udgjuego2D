using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum ControllerType
{
    PC,
    MOBILE
}

public class GameManagment : MonoBehaviour
{
    public ControllerType controller;
    bool GameOver = false;
    public float restartDelay = 1f;
    public GameObject completeLevelUI;
    public GameObject mobileControllerUI;
    public JohnMovement John;
    public TMP_Dropdown dropdown;

    // Update is called once per frame
    void Start()
    {
        controller = ControllerType.PC;
    }

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

    public void ControllerSetUp()
    {
        if (dropdown.value == 0)
        {
            controller = ControllerType.PC;
            mobileControllerUI.SetActive(false);
        }
        if (dropdown.value == 1)
        {
            controller = ControllerType.MOBILE;
            mobileControllerUI.SetActive(true);
        }

        John.controller = controller;
    }
}
