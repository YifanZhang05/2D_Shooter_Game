using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    public void loadNewGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void loadGameOverScene()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void loadMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
