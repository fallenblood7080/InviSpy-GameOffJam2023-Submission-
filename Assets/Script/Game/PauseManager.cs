using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;
using Extension;

/// <summary>
/// Respoonsible for managing Game Pause State
/// </summary>
public class PauseManager : MonoBehaviour
{
    private static PauseManager instance;
    /// <summary>
    /// Instance of PauseManager
    /// </summary>
    public static PauseManager GetInstance => instance;

    /// <summary>
    /// which should i press to pause?
    /// </summary>
    [SerializeField, Tooltip("Which Should I Press to Pause?")] private InputAction pauseActionBinding;
    [Space(5f)]

    
    [SerializeField] private UnityEvent onGamePaused, onGameUnpaused; //!Events which get Invoke when game get pause or unpaused!

    public UnityEvent OnGamePaused => onGamePaused;
    public UnityEvent OnGameUnpaused => onGameUnpaused;

    bool isPaused;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        OnGamePaused.AddListener(PauseGame);
        OnGameUnpaused.AddListener(UnPauseGame);
        pauseActionBinding.Enable();
    }
    private void Update()
    {
        //! if pause pressed
        if (pauseActionBinding.WasPerformedThisFrame())
        {
            "BackPressed".Log();
            HandlePause();
        }
    }

    public void HandlePause()
    {
        if (isPaused)
        {
            OnGameUnpaused?.Invoke();
        }
        else
        {
            OnGamePaused?.Invoke();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            onGamePaused?.Invoke();
        }
    }
    private void PauseGame()
    {
        "GameIsPaused".Log("00ff00", 16);
        isPaused = true;
        Time.timeScale = 0;
    }
    private void UnPauseGame()
    {
        "GameIs-UN-Paused".Log("00ff00", 16);
        isPaused = false;
        Time.timeScale = 1;
    }
}
