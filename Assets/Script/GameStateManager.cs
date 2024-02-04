using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public enum GameState
    {
        MainMenu,       
        InGame,
        Summary,
        Paused,
        GameOver,
        // Add any other states you need
    }

    public static GameStateManager Instance;
    public GameState currentGameState;


    public int PlayerCurrence;

    public int BasePaid = 3600;
    public int bonus = 120;
    public int deduction  = -180;
    public int extraBonus = 500;
    public int totalToday;

    public int currentBonus;
    public int currentDeduction;

    public int Days = 1;

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

    private void Start()
    {
        // Initial state
        ChangeGameState(GameState.MainMenu);
        currentBonus = 0;
        currentDeduction = 0;
        Days = 1;
    }

    private void Update()
    {
       
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }
    public void ChangeGameState(GameState newGameState)
    {
        currentGameState = newGameState;

        switch (currentGameState)
        {
            case GameState.MainMenu:
                // Operations needed in the MainMenu state
                break;
            case GameState.InGame:
                // Operations needed in the InGame state
                Time.timeScale = 1;
                break;
            case GameState.Summary:
                break;
            case GameState.Paused:
                // Operations needed in the Paused state
                Time.timeScale = 0;
                break;
            case GameState.GameOver:
                // Operations needed in the GameOver state
                break;
                // Handle other states
        }
    }
}
