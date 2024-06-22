using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using SuperPupSystems.Helper;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        #if(UNITY_EDITOR)
        Debug.Log("Quiting Play Mode");
        EditorApplication.ExitPlaymode();
        #else
        Debug.Log("Quitting Build");
        Application.Quit();
        #endif
    }
}
