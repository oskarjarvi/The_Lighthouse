using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuScript : MonoBehaviour
{
    public Animator fadeTransition;
    public float transitionTime = 1f;
    [SerializeField]
    private CursorManager cursorManager;

    public void PlayGame()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        fadeTransition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
        cursorManager.SetCursorState(false);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
