using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused;

    [SerializeField]
    private CursorManager cursorManager;

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

    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        cursorManager.SetCursorState(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

