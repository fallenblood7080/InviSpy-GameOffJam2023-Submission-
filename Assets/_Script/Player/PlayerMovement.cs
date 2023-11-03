using Extension;
using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] private Transform cameraFollowRoot;
    [SerializeField] private Transform ceilingCheck;
    [Space(5f)]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float jumpHeight;
    [Space(5f)]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private bool followY;

    private float currentSpeed;
    private Vector3 dir;
    private bool isCrouched;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Start()
    {
        currentSpeed = moveSpeed;
        if(cameraFollowRoot == null)
        {
            Debug.LogError("cameraFollowRoot is not Assigned!");
        }
    }

    private void Update()
    {
        if (controller.isGrounded) dir.y = -2f;

        cameraFollowRoot.transform.position = followY ? transform.position : new(transform.position.x, 0, transform.position.z);

        dir = new(InputManager.GetInstance.MoveInput.x * currentSpeed, dir.y, InputManager.GetInstance.MoveInput.y * currentSpeed);

        if (dir.sqrMagnitude > 0.1) RotatePlayerTowardMovingDir(dir);

        if (controller.isGrounded)
        {
            if (InputManager.GetInstance.IsJumpPressed)
            {
                Action playerAction = isCrouched ? Stand : Jump;
                playerAction();
            }
            if (InputManager.GetInstance.IsCrouchPressed)
            {
                Action playerAction = isCrouched ? Stand : Crouch;
                playerAction();
            } 
        }

        dir.y += gravity * Time.deltaTime;
        controller.Move(Time.deltaTime * dir);
    }

    private void Jump()
    {
        dir.y = Mathf.Sqrt(gravity * -2 * jumpHeight);
        "Jumped".Log();
    }

    private void Crouch()
    {
        isCrouched = true;
        currentSpeed = crouchSpeed;
        "Crouched".Log();
    }

    private void Stand()
    {
        isCrouched = false;
        currentSpeed = moveSpeed;
        "Standed".Log();
    }

    void RotatePlayerTowardMovingDir(Vector3 dir)
    {
        Quaternion targetRotation = Quaternion.LookRotation(new(dir.x,0,dir.z));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * 100 * Time.deltaTime);
    }
}
