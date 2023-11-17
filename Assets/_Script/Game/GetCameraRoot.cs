using Cinemachine;
using UnityEngine;

public class GetCameraRoot : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private Transform cameraRoot;
    private PlayerMovement movement;

    [SerializeField] private bool isCamera;

    private void Awake()
    {
        if(isCamera)
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }
        else
        {
            movement = GetComponent<PlayerMovement>();
        }
    }

    private void Start()
    {
        cameraRoot = GameObject.FindGameObjectWithTag("CameraRoot").transform;
        if (isCamera)
        {
            virtualCamera.m_Follow = cameraRoot;
            virtualCamera.m_LookAt = cameraRoot; 
        }
        else
        {
            movement.CameraFollowRoot = cameraRoot;
        }
    }
}
