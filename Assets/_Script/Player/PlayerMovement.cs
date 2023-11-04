using Extension;
using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] private Transform cameraFollowRoot;
    [SerializeField] private Transform ceilingCheck;
    [Space(5f)]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float crouchSpeed;
    [Space(2f)]
    [SerializeField] private float smallSizeMoveSpeed;
    [SerializeField] private float smallSizeCrouchSpeed;
    [Space(2f)]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float crouchHeight = 1f, standHeight = 2f;
    [Space(5f)]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private bool followY;
    [SerializeField] private LayerMask playerLayer;

    [Space(5f)]
    [SerializeField] private Animator playerAnimator;

    [SerializeField] private UnityEvent OnJumpLanded;

    private NoiseHandler noise;
    private ShapeShiftPower shiftPower;

    private float currentSpeed;
    private Vector3 dir;
    private bool isCrouched;
    private bool isMoving;
    private bool isSthAbove;
    

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        noise = GetComponent<NoiseHandler>();
        shiftPower = GetComponent<ShapeShiftPower>();
    }
    private void Start()
    {
        currentSpeed = moveSpeed; //! intialse the player speed
        controller.height = standHeight;
        if(cameraFollowRoot == null)
        {
            Debug.LogError("cameraFollowRoot is not Assigned!");
        }
    }

    private void Update()
    {
        currentSpeed = shiftPower.IsCurrentlySmall ? (isCrouched ? smallSizeCrouchSpeed : smallSizeMoveSpeed) : (isCrouched ? crouchSpeed : moveSpeed);


        if (controller.isGrounded) dir.y = -2f; //! to prevent further increasing on dir.y will force it to be -2

        isSthAbove = Physics.CheckSphere(ceilingCheck.position, 0.3f, ~playerLayer); //! is there sth above player?

        cameraFollowRoot.transform.position = followY ? transform.position : new(transform.position.x, 0, transform.position.z); //! check whether the camera needs to follow y axis of player then according to it follows

        dir = new(InputManager.GetInstance.MoveInput.x * currentSpeed, dir.y, InputManager.GetInstance.MoveInput.y * currentSpeed); //! reads the input

        if (dir.x != 0 || dir.z != 0) //! is there any movement, if yes then rotate the player towards that direction
        {
            RotatePlayerTowardMovingDir(dir);
            isMoving = true;
            if(shiftPower.IsCurrentlySmall)
                noise.CreateNoise(); //! Create some noise on movement
        }
        else
        {
            isMoving = false;
        }

        if (controller.isGrounded && !isSthAbove)//! if on ground and there is nothing above the player
        {
            if (InputManager.GetInstance.IsJumpPressed) 
            {
                //! is player is in crouch position? If yes, then stand else jump
                Action playerAction = isCrouched ? Stand : Jump; 
                playerAction();
            }
            if (InputManager.GetInstance.IsCrouchPressed)
            {
                //! is player is in crouch position? If yes, then stand else crouch
                Action playerAction = isCrouched ? Stand : Crouch;
                playerAction();
            } 
        }

        playerAnimator.SetBool("isGround", controller.isGrounded);
        playerAnimator.SetBool("isCrouching", isCrouched);
        playerAnimator.SetBool("isMoving", isMoving);

        dir.y += gravity * Time.deltaTime; //! Add some gravity
        controller.Move(Time.deltaTime * dir); //! remember g is acceleration value thats why you multiply time.deltatime twice (m/s^2) and also for movement
    }

    private void LateUpdate()
    {
        //! Fixing the Animation error
        playerAnimator.transform.position = transform.root.position;
        playerAnimator.transform.rotation = transform.root.rotation;
    }

    private void Jump()
    {
        dir.y = Mathf.Sqrt(gravity * -2 * jumpHeight);
        "Jumped".Log();
        float timeToReachGround = 2 * Mathf.Sqrt(2 * jumpHeight / -gravity);//! Calcuate the time required to land on ground: t = 2√(2h/g)
        Invoke(nameof(JumpLanded), timeToReachGround); //! Invoke jump landed after calculate time
    }

    private void Crouch()
    {
        isCrouched = true;
        currentSpeed = crouchSpeed;
        "Crouched".Log();
        controller.height = crouchHeight;
    }

    private void Stand()
    {
        isCrouched = false;
        currentSpeed = moveSpeed;
        controller.height = standHeight;
        "Standed".Log();
    }

    void RotatePlayerTowardMovingDir(Vector3 dir)
    {
        Quaternion targetRotation = Quaternion.LookRotation(new(dir.x,0,dir.z));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * 100 * Time.deltaTime); //! Smooth the rotation
    }

    void JumpLanded()
    {
        //? wrap noise method with issmall condition
        if (!shiftPower.IsCurrentlySmall)
        {
            noise.CreateNoise(); 
        }
        OnJumpLanded?.Invoke(); //! Invoke OnJumpLand event
    }
}
