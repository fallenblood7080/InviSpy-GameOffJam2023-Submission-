
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private static CameraShake instance;
    public static CameraShake GetInstance => instance;

    private CinemachineVirtualCamera virtualCamera;
    private float shakeTime;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            if (shakeTime <= 0)
            {
                CinemachineBasicMultiChannelPerlin noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                noise.m_AmplitudeGain = 0;
                noise.m_FrequencyGain = 0;
            }
        }
    }
    public void Shake(float intensity ,float timer, float freq)
    {
        CinemachineBasicMultiChannelPerlin noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = intensity;
        noise.m_FrequencyGain = freq;
        shakeTime = timer;
    }
}
