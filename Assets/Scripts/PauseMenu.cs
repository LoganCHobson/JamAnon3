using SuperPupSystems.Manager;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public bool isGamePaused = false;

    public GameObject pauseMenuUI;
    public GameObject howToPlayUI;
    public GameObject optionsUI;
    public GameObject hudUI;

    public TMP_Text moneyText;
    public TMP_Text attempts;
    public TMP_Text scoreText;


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
        howToPlayUI.SetActive(false);
        optionsUI.SetActive(false);
        hudUI.SetActive(true);
        Time.timeScale = 1.0f;
        isGamePaused = false;
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        hudUI.SetActive(false);
        scoreText.text = ScoreManager.instance.score.ToString();
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

    public void Toggle(GameObject obj)
    {
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);
        obj.SetActive(!obj.activeSelf);
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
