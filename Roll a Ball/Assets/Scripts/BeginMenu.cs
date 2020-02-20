using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void HelpMenu()
    {
        SceneManager.LoadScene("Help Menu");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}

