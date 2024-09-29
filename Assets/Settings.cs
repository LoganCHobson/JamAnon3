using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    //This script is legit just a bus for information.
    public static Settings instance;

    public float mouseSense = 500;
    public float fov = 60;
    public bool toggleOn = true;
    public int resolutionIndex;
    private void Awake()
    {
        mouseSense = 500;
        fov = 60;
        toggleOn = true;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
    }

    void OnActiveSceneChanged(Scene previousScene, Scene newScene)
    {
        Debug.Log("Scene changed from " + previousScene.name + " to " + newScene.name);
    }
}
