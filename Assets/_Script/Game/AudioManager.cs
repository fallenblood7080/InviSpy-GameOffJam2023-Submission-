using Extension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        GetComponent<AudioSource>().PlayFadeInOut(5, 2, EaseType.EaseIn, EaseType.EaseOut, 0.25f);
    }
}
