using Extension;
using UnityEngine;
using UnityEngine.Events;

public class NoiseHandler : MonoBehaviour
{
    [SerializeField] private float noiseHearingRange;
    private UnityEvent onNoiseCreate;

    #region PROPERTY
    public float NoiseHearingRange => noiseHearingRange;
    #endregion

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
            if (Vector3.Distance(transform.position, enemies.transform.position) <= noiseHearingRange && !EnemyManager.Instance.playerPower.IsCurrentlySmall)
            {
                onNoiseCreate?.AddListener(OnCreateNoise);
            }
            else
            {
                onNoiseCreate?.AddListener(OnEndCreatingNoise);
            }
        }
    }

    public void OnCreateNoise()
    {
        EnemyManager.Instance.isCreatingNoise = true;
    }

    public void OnEndCreatingNoise()
    {
        EnemyManager.Instance.isCreatingNoise = false;
    }

    private static readonly string ENEMY_TAG = "Enemy";

}