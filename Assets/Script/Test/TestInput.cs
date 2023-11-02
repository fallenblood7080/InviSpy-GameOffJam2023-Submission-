using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour
{
    void Update()
    {
        if (InputManager.GetInstance.IsJumpPressed)
        {
            Debug.Log("Jump Pressed!");
        }
        if (InputManager.GetInstance.IsCrouchPressed)
        {
            Debug.Log("Crouch Pressed!");
        }
        if (InputManager.GetInstance.IsChangeSizePressed)
        {
            Debug.Log("Size Change Pressed");
        }
    }
}
