using Extension;
using System.Linq;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [Header("Footstep")]
    [SerializeField] private AudioClip[] footsteps; //! Contains the list of footstep audio
    [SerializeField] private AudioClip jumplandedClip;

    private AudioSource audioSource;

    private void Start() 
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    public void OnFootstep()
    {
        AudioClip clip = footsteps.ToList().GetRandomItems()[0]; //!Gets the Random Audio from footsteps array
        audioSource.PlayOneShot(clip); //!Play the Audio
    }

    public void OnJumpLanded()
    {
        AudioClip clip = jumplandedClip;
        audioSource.PlayOneShot(jumplandedClip);
    }
}