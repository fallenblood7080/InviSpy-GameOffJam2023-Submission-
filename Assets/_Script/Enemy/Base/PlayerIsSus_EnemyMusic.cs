using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIsSus_EnemyMusic : MonoBehaviour
{
    [SerializeField] private AudioSource playerIsSus;
    public bool isplayerIsSusPlaying = false;
    public void PlayMusic()
    {
        isplayerIsSusPlaying = true;
        playerIsSus.Play();
        StartCoroutine(StopMusic());
    }

    private IEnumerator StopMusic()
    {
        yield return new WaitForSeconds(22f);
        isplayerIsSusPlaying = false;
    }
}