using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void LaunchGame(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
