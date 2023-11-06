using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [Header("Footstep")]
    [SerializeField] private AudioClip[] footsteps;


    private AudioSource audioSource;

    private void Start() 
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    public void OnFootstep()
    {
        AudioClip clip = GetRandomFootstepClip();
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomFootstepClip()
    {
        return footsteps[Random.Range(0, footsteps.Length)];
    }
}