using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using SuperPupSystems.Helper;

public class DeathScreen : MonoBehaviour
{
     public bool didDie = false;

    public Health playerHealth;

    public GameObject deathSection;

    //public GameObject healthBar;
    public GameObject reticle;

    void Start()
    {
        deathSection.SetActive(false);
    }

    public void Died()
    {
        Debug.Log("You have Died!");
        didDie = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        deathSection.SetActive(true);
        //healthBar.SetActive(false);
        reticle.SetActive(false);
        Time.timeScale = 0.0f;
    }

    public void Retry()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        Debug.Log("Loading Main Menu");
        //Time.timeScale = 1.0f;
        //SceneManager.LoadSceneAsync("MainMenu");
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
