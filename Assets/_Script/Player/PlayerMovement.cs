using Extension;
using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    [field: SerializeField] public Transform CameraFollowRoot { get; set; }
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
    [SerializeField] private bool shouldInverseInput;

    [Space(5f)]
    [SerializeField] private Animator playerAnimator;
    [Space(5f)]
    [SerializeField] private ParticleSystem _jumpParticles;
    [SerializeField] private ParticleSystem _landParticles;

    [SerializeField] private UnityEvent OnJumpLanded;

    private NoiseHandler noise;
    private ShapeShiftPower shiftPower;

    private float currentSpeed;
    private Vector3 dir;
    private bool isCrouched;
    public bool IsMoving { get; private set; }
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
        if(CameraFollowRoot == null)
        {
            Debug.LogError("cameraFollowRoot is not Assigned!");
        }
    }

    private void Update()
    {
        currentSpeed = shiftPower.IsCurrentlySmall ? (isCrouched ? smallSizeCrouchSpeed : smallSizeMoveSpeed) : (isCrouched ? crouchSpeed : moveSpeed);

        if (controller.isGrounded) dir.y = -2f; //! to prevent further increasing on dir.y will force it to be -2

        isSthAbove = Physics.CheckSphere(ceilingCheck.position, 0.3f, ~playerLayer); //! is there sth above player?

        CameraFollowRoot.transform.position = followY ? transform.position : new(transform.position.x, 0, transform.position.z); //! check whether the camera needs to follow y axis of player then according to it follows

        if (!shouldInverseInput)
        {
            dir = new(InputManager.GetInstance.MoveInput.x * currentSpeed, dir.y, InputManager.GetInstance.MoveInput.y * currentSpeed); //! reads the input
        }
        else
        {
            dir = new(-InputManager.GetInstance.MoveInput.x * currentSpeed, dir.y, -InputManager.GetInstance.MoveInput.y * currentSpeed); //! reads the input
        }
        if (dir.x != 0 || dir.z != 0) //! is there any movement, if yes then rotate the player towards that direction
        {
            RotatePlayerTowardMovingDir(dir);
            IsMoving = true;
            if(!shiftPower.IsCurrentlySmall && !isCrouched)
                noise.CreateNoise(); //! Create some noise on movement
        }
        else
        {
            IsMoving = false;
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

        playerAnimator.SetBool(ISGROUND_TAG, controller.isGrounded);
        playerAnimator.SetBool(ISCROUCH_TAG, isCrouched);
        playerAnimator.SetBool(ISMOVE_TAG, IsMoving);

        dir.y += gravity * Time.deltaTime; //! Add some gravity
        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack") == false)
        {
            controller.Move(Time.deltaTime * dir); 
        } //! remember g is acceleration value thats why you multiply time.deltatime twice (m/s^2) and also for movement
    }

    private void LateUpdate()
    {
        //! Fixing the Animation error
        playerAnimator.transform.SetPositionAndRotation(transform.root.position, transform.root.rotation);
    }

    private void Jump()
    {
        dir.y = Mathf.Sqrt(gravity * -2 * jumpHeight);
        _jumpParticles.Play();
        float timeToReachGround = 2 * Mathf.Sqrt(2 * jumpHeight / -gravity);//! Calcuate the time required to land on ground: t = 2√(2h/g)
        Invoke(nameof(JumpLanded), timeToReachGround); //! Invoke jump landed after calculate time
    }

    private void Crouch()
    {
        isCrouched = true;
        currentSpeed = crouchSpeed;
        controller.height = crouchHeight;
    }

    public void Stand()
    {
        isCrouched = false;
        currentSpeed = moveSpeed;
        controller.height = standHeight;
    }

    void RotatePlayerTowardMovingDir(Vector3 dir)
    {
        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack") == false)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new(dir.x, 0, dir.z));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * 100 * Time.deltaTime);  
        }//! Smooth the rotation
    }

    void JumpLanded()
    {
        if (!shiftPower.IsCurrentlySmall)
        {
            noise.CreateNoise(); 
        }
        OnJumpLanded?.Invoke(); 
        _landParticles.Play();
        
    }

    #region  Cached Properties
    
    private static readonly string ISGROUND_TAG = "isGround";
    private static readonly string ISCROUCH_TAG = "isCrouching";
    private static readonly string ISMOVE_TAG = "isMoving";

    #endregion
}