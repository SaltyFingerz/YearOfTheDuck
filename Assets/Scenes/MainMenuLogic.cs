using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    public void TestMessage() {
        Debug.Log("TEST");
    }

    public void OptionPlay()
    {
        SceneManager.LoadScene("Level1-1");
    }

    public void OptionQuit()
    {
        Application.Quit();
    }
}
