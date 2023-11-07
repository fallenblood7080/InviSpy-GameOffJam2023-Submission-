using Extension;
using UnityEngine;
using UnityEngine.Events;

public class NoiseHandler : MonoBehaviour
{
    [field:SerializeField] public float noiseHearingRange {get; private set;}
    private UnityEvent onNoiseCreate;

    public void CreateNoise()
    {
        GetAllListenerNearby();
        onNoiseCreate?.Invoke();
    }

    private void GetAllListenerNearby()
    {
        onNoiseCreate?.RemoveAllListeners();

        foreach (GameObject enemies in GameObject.FindGameObjectsWithTag(ENEMY_TAG))
        {
            if (Vector3.Distance(transform.position, enemies.transform.position) <= noiseHearingRange)
            {
                onNoiseCreate?.AddListener(OnCreateNoise);
            }
        }
    }

    public void OnCreateNoise()
    {
        Debug.Log("Detect");
    }

    private static readonly string ENEMY_TAG = "Enemy";
}