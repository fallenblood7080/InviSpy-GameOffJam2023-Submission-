using Extension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource inGameMusic;
    private void Start()
    {
        inGameMusic.PlayFadeInOut(5, 2, EaseType.EaseIn, EaseType.EaseOut, 0.25f);
    }

    private void Update()
    {
        if (GetComponent<PlayerIsSus_EnemyMusic>() != null)    
        {
            if (GetComponent<PlayerIsSus_EnemyMusic>().isplayerIsSusPlaying == true)
            {
                inGameMusic.Pause();
            }

            if (GetComponent<PlayerIsSus_EnemyMusic>().isplayerIsSusPlaying == false)
            {
                inGameMusic.UnPause();
            }
        }
    }
}
