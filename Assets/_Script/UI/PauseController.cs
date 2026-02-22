using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// NRE - Null Reference Exception
// An error that occurs when trying to access a member on a type that is null.
public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button buttonMainMenu;

    private void OnEnable()
    {
        if (EventBus.Instance != null)
        {
            EventBus.Instance.OnGamePaused += ShowPausePanel;
            EventBus.Instance.OnGameResumed += HidePausePanel;
        }
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnPausePressed += HandlePausePressed;
            InputManager.Instance.OnCancelPressed += HandleCancelPressed;
        }
    }

    private void OnDisable()
    {
        if (EventBus.Instance != null)
        {
            EventBus.Instance.OnGamePaused -= ShowPausePanel;
            EventBus.Instance.OnGameResumed -= HidePausePanel;
        }
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnPausePressed -= HandlePausePressed;
            InputManager.Instance.OnCancelPressed -= HandleCancelPressed;
        }
    }

    void HandlePausePressed()
    {
        if (GameManager.Instance != null && GameManager.Instance.CurrentGameState == GameState.Playing)
        {
            GameManager.Instance.Pause();
        }
    }

    void HandleCancelPressed()
    {
        if (GameManager.Instance != null && GameManager.Instance.CurrentGameState == GameState.Paused)
        {
            GameManager.Instance.Resume();
        }
    }

    private void Start()
    {
        resumeButton.onClick.AddListener(OnResumeClicked);
        buttonMainMenu.onClick.AddListener(OnMainMenuClicked);
    }

    void ShowPausePanel()
    {
        if (pausePanel != null)
            pausePanel.SetActive(true);
    }

    void HidePausePanel()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    void OnResumeClicked()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.Resume();
    }

    void OnMainMenuClicked()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.GoToMenu();
    }
}
