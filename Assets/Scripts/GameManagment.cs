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

    public GameObject mobileChangeUI;
    public GameObject pcChangeUI;

    public JohnMovement John;

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

    public void ControllerSetUp(int value)
    {
        if (value == 1)
        {
            ActivateMobileControllerUI();
        }
         if (value == 0)
        {
            ActivatePCControllerUI();
        }
    }

    public void ActivateMobileControllerUI()
    {
        controller = ControllerType.MOBILE;
        mobileControllerUI.SetActive(true);
        mobileChangeUI.SetActive(false);
        pcChangeUI.SetActive(true);
        John.controller = controller;
    }

    public void ActivatePCControllerUI()
    {
        controller = ControllerType.PC;
        mobileControllerUI.SetActive(false);
        pcChangeUI.SetActive(false);
        mobileChangeUI.SetActive(true);
        John.controller = controller;
    }
}
