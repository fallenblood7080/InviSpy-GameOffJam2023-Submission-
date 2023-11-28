using Extension;
using UnityEngine;
using UnityEngine.Events;

public class NoiseHandler : MonoBehaviour
{
    [SerializeField] private float noiseHearingRange;
    private UnityEvent<Vector3, int> onNoiseCreate;

    #region PROPERTY
    public float NoiseHearingRange => noiseHearingRange;
    #endregion

    public void CreateNoise(int noisePwr)
    {
        GetAllListenerNearby();
        onNoiseCreate?.Invoke(transform.position,noisePwr);
    }

    private void GetAllListenerNearby()
    {
        onNoiseCreate?.RemoveAllListeners();

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag(ENEMY_TAG))
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= noiseHearingRange)
            {
                onNoiseCreate?.AddListener(enemy.GetComponent<Enemy>().OnHearNoise);

            }
            else
            {
                onNoiseCreate?.AddListener(enemy.GetComponent<Enemy>().OnEndHearingNoise);
            }
        }
    }

    private static readonly string ENEMY_TAG = "Enemy";

}