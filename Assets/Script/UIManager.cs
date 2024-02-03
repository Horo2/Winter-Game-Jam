using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;


    public string gameSceneName;
    public string mainMenuSceneName;

    public static UIManager Instance;
    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == mainMenuSceneName)
        {
            GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.MainMenu);
            mainMenu.SetActive(true);
        }
        else
        {
            mainMenu.SetActive(false);
        }
    }

    public void StartGame()
    {
        GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.InGame);
        SceneManager.LoadScene(gameSceneName);
        //deploymentMenu.SetActive(true);
        mainMenu.SetActive(false);

    }
    public void LeaveGame()
    {
        Application.Quit();
    }
}
