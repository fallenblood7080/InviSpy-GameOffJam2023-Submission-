using Extension;
using UnityEngine;
using UnityEngine.Events;

public class NoiseHandler : MonoBehaviour
{
    [SerializeField] private float noiseHearingRange;
    private UnityEvent onNoiseCreate;

    public void CreateNoise()
    {
        GetAllListenerNearby();
        onNoiseCreate?.Invoke();
    }

    private void GetAllListenerNearby()
    {
        onNoiseCreate?.RemoveAllListeners();

        foreach (GameObject enemies in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (Vector3.Distance(transform.position, enemies.transform.position) <= noiseHearingRange)
            {
                //onNoiseCreate.?AddListener();
            }
        }
    }
}