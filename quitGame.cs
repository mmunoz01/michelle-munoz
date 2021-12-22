using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class quitGame : MonoBehaviour
{

    public void QuitGame()
    {
        Debug.Log("You left the game");
        Application.Quit();

    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }

}
