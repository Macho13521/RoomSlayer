using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
  

    public void startGame()
    {
        SceneManager.LoadScene("GRA");
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void goToMenu()
    {
        SceneManager.LoadScene("ScenaPierwsza");
    }
}
