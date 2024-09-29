using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using SuperPupSystems.Helper;
using UnityEditor.Rendering;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject mainMenu;

    public Slider loadSlider;
    public void LoadLevel(string level)
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);
        StartCoroutine(LoadLevelAsync(level));
    }
    IEnumerator LoadLevelAsync(string level)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(level);

        while(!load.isDone)
        {
            float progress = Mathf.Clamp01(load.progress / 0.9f);
            loadSlider.value = progress;
            yield return null;
        }
    }

    public void Toggle(GameObject obj)
    {
        mainMenu.SetActive(!mainMenu.activeSelf);   
        obj.SetActive(!obj.activeSelf);
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
