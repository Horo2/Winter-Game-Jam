using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject pauseMenu;


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
            pauseMenu.SetActive(false);
        }
        else
        {
            mainMenu.SetActive(false);
        }
    }

    private void Update()
    {
        if (GameStateManager.Instance.currentGameState == GameStateManager.GameState.InGame)
        {
            Debug.Log("”Œœ∑÷–£°£°£°£°£°");
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //Stop the game
                GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.Paused);
                Cursor.lockState = CursorLockMode.None;
                pauseMenu.SetActive(true);
            }
        }
        else if (GameStateManager.Instance.currentGameState == GameStateManager.GameState.Paused)
        {
            Debug.Log("‘›Õ£÷–£°£°£°£°");
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //Back to in game
                GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.InGame);
                Cursor.lockState = CursorLockMode.Locked;
                pauseMenu.SetActive(false);
            }
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

    public void ReturnMainMenu()
    {
        GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.MainMenu);
        SceneManager.LoadScene(mainMenuSceneName);
        mainMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void ContinueButton()
    {
        GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.InGame);
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
    }
}
