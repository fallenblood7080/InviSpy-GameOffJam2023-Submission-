using Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOverManager : MonoBehaviour
{
    private static GameOverManager instance;
    public static GameOverManager GetInstance => instance;

    [SerializeField] private UnityEvent<bool> onGameOver;
    public UnityEvent<bool> OnGameOver => onGameOver;

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
    }

    private void MissionSuccessful()
    {
        //TODO: show mission Succesful, go to next mission
        "Mission Successful".Log("0000ff", 18);
    }
}
