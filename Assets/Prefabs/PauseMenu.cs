using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool paused = false;

    Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            paused = !paused;
            if (paused)
            {
                Time.timeScale = 0;
            }
            else {
                Time.timeScale = 1;
            }
        }
        canvas.enabled = paused;
    }

    public void OptionReset() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        paused = false;
    }

    public void OptionQuitToMenu() {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
        paused = false;
    }
}
