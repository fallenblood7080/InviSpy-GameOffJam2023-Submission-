using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour
{
    void Update()
    {
        if (InputManager.GetInstance.MoveInput.magnitude > 0.001)
        {
            Debug.Log($"X:{InputManager.GetInstance.MoveInput.x}  Y:{InputManager.GetInstance.MoveInput.y}")
        }
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
