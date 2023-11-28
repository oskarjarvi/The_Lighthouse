using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused;

    [SerializeField]
    private CursorManager cursorManager;
    [SerializeField]
    private GameObject interactionCanvas;

    void Start()
    {
        pauseMenu.SetActive(false);
    }
    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {

            PauseGame();
        }


    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        cursorManager.SetCursorState(true);
        interactionCanvas.SetActive(false);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        cursorManager.SetCursorState(false);
        interactionCanvas.SetActive(true);
    }

    public void ExitGame()
    {
        LoadMainMenu();
    }

    void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        cursorManager.SetCursorState(false);
    }
}

