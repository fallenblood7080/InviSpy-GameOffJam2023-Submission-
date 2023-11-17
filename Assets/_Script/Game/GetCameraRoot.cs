using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCameraRoot : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private Transform cameraRoot;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        cameraRoot = GameObject.FindGameObjectWithTag("CameraRoot").transform;
        virtualCamera.m_Follow = cameraRoot;
        virtualCamera.m_LookAt = cameraRoot;
    }
}
