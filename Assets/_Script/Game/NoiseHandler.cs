using Extension;
using UnityEngine;
using UnityEngine.Events;

//attach this script to that component which will create noise
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
        onNoiseCreate.AddListener(Noisetest);
/*        foreach (GameObject enemies in GameObject.FindGameObjectsWithTag(""))
        {
            if (Vector3.Distance(transform.position, enemies.transform.position) <= noiseHearingRange)
            {
                //onNoiseCreate.AddListener();
            }
        }*/
    }

    private void Noisetest()
    {
        "Some Noise!!".Log();
    }
}
