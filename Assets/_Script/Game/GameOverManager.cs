using Extension;
using UnityEngine;
using UnityEngine.Events;

public class GameOverManager : MonoBehaviour
{
    private static GameOverManager instance;
    public static GameOverManager GetInstance => instance;

    [SerializeField] private UnityEvent<bool> onGameOver;
    public UnityEvent<bool> OnGameOver => onGameOver;

    [SerializeField] private GameObject failedObject, successObject;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        onGameOver?.AddListener(GameOver);
    }

    void GameOver(bool isPassed)
    {
        Cursor.lockState = CursorLockMode.None;
        if (isPassed)
        {
            MissionSuccessful();
        }
        else
        {
            MissionFailed();
        }
    }

    private void MissionFailed()
    {
        //TODO: Show mission failed
        "Mission Failed".Log("ff0000", 18);
        failedObject.SetActive(true);
    }

    private void MissionSuccessful()
    {
        //TODO: show mission Succesful, go to next mission
        "Mission Successful".Log("0000ff", 18);
        successObject.SetActive(false);
    }
}
