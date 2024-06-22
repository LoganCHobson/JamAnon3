using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using SuperPupSystems.Helper;

public class DeathScreen : MonoBehaviour
{

    public void Died()
    {
        Debug.Log("You have Died!");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // public void Retry()
    // {
    //     Time.timeScale = 1.0f;
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    // }

    // public void LoadMenu()
    // {
    //     Debug.Log("Loading Main Menu");
    //     Time.timeScale = 1.0f;
    //     SceneManager.LoadSceneAsync("MainMenu");
    // }

    // public void QuitGame()
    // {
    //     #if(UNITY_EDITOR)
    //     Debug.Log("Quiting Play Mode");
    //     EditorApplication.ExitPlaymode();
    //     #else
    //     Debug.Log("Quitting Build");
    //     Application.Quit();
    //     #endif
    // }
}
