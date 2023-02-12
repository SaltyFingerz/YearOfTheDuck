using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool paused = false;

    public CanvasGroup canvGroup;

    void Start()
    {
        setPauseState(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            setPauseState(!paused);
        }
    }

    void setPauseState(bool p)
    {
        paused = p;

        if (p)
        {
            Time.timeScale = 0;

            canvGroup.alpha = 1;
            canvGroup.interactable = true;
            canvGroup.blocksRaycasts = true;
        }
        else
        {
            Time.timeScale = 1;

            canvGroup.alpha = 0;
            canvGroup.interactable = false;
            canvGroup.blocksRaycasts = false;
        }
    }

    public void OptionReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        paused = false;
    }

    public void OptionQuitToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
        paused = false;
    }

    public void OptionBackToGame()
    {
        setPauseState(false);
    }
}
