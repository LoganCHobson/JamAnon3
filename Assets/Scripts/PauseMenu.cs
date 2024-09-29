using SuperPupSystems.Manager;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public bool isGamePaused = false;

    public GameObject pauseMenuUI;
    public GameObject hud;

    public TMP_Text moneyText;
    public TMP_Text attempts;


    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Debug.Log("Resume Button Was Hit");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        hud.SetActive(true);
        Time.timeScale = 1.0f;
        isGamePaused = false;
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        hud.SetActive(false);
        attempts.text = GameManager.instance.runCounter.runCounter.ToString();
        moneyText.text = WalletManager.instance.coin.ToString();
        Time.timeScale = 0.0f;
        isGamePaused = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Loading Main Menu");
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void QuitGame()
    {
#if (UNITY_EDITOR)
        Debug.Log("Quiting Play Mode");
        EditorApplication.ExitPlaymode();
#else
        Debug.Log("Quitting Build");
        Application.Quit();
#endif
    }
}
