using Unity.VisualScripting;
using UnityEngine;

public enum GameState 
{
    MainMenu = 0,
    Playing = 1,
    Paused = 2,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState CurrentGameState { get; private set; } = GameState.MainMenu;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void StartGame()
    {
        CurrentGameState = GameState.Playing;
        Time.timeScale = 1f;
        SceneLoader.Instance.Load(SceneNames.GameScene);
        if (InputManager.Instance != null)
        {
            InputManager.Instance.EnablePlayerInput();
        }
        Debug.Log("Game Started");
    }

    public void GoToMenu()
    {
        CurrentGameState = GameState.MainMenu;
        Time.timeScale = 1f;
        SceneLoader.Instance.Load(SceneNames.MainMenu);
        if (InputManager.Instance != null)
        {
            InputManager.Instance.EnableUIInput();
        }
        Debug.Log("Returned to Main Menu");
    }

    public void Pause()
    {
        if (CurrentGameState != GameState.Playing)
            return;

        CurrentGameState = GameState.Paused;
        Time.timeScale = 0f;
        EventBus.Instance.RaiseGamePaused();
        Debug.Log("Game Paused");
    }

    public void Resume()
    {
        if (CurrentGameState != GameState.Paused)
            return;

        CurrentGameState = GameState.Playing;
        Time.timeScale = 1f;
        EventBus.Instance.RaiseGameResumed();
        Debug.Log("Game Resumed");
    }
}
