using UnityEngine;

/// <summary>
/// Responsible for Reading the Input from Device.
/// Using Singleton Pattern <br></br>
///Currently Defined Movement are MoveInput(Vector2), IsJumpPressed(bool), IsCrouchedPressed(bool), IsChangeSizePressed(bool)
/// </summary>
public class InputManager : MonoBehaviour
{
    private static InputManager instance;



    private GameInputActionMap gameaActionMap;
    private GameInputActionMap.PlayerActions playerActions;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool isJumpPressed;
    private bool isCrouchPressed;
    private bool isChangeSizePressed;
    private bool isInteractPressed;

    #region PROPERTY
    /// <summary>
    /// Return the instance of InputManager
    /// </summary>
    public static InputManager GetInstance => instance;
    public Vector2 MoveInput => moveInput;
    public bool IsJumpPressed => isJumpPressed;
    public bool IsCrouchPressed => isCrouchPressed;
    public bool IsChangeSizePressed => isChangeSizePressed;
    public bool IsInteractPressed => isInteractPressed;
    public Vector2 LookInput => lookInput;
    #endregion

    private void Awake() => Intialization();
    private void OnEnable() => EnablePlayerInput();
    private void OnDisable() => DisablePlayerInput();

    private void Update()
    {
        if (!playerActions.enabled) //! if input are disable then dont read input unnecessarily
            return;

        moveInput = playerActions.Move.ReadValue<Vector2>().normalized; 
        isJumpPressed = playerActions.Jump.WasPerformedThisFrame();
        //isCrouchPressed = playerActions.Crouch.WasPerformedThisFrame();
        isChangeSizePressed = playerActions.SizeChange.WasPerformedThisFrame();
        isInteractPressed = playerActions.Interact.WasPerformedThisFrame();
        lookInput = playerActions.Look.ReadValue<Vector2>().normalized;
    }

    private void Intialization()
    {
        if (instance == null)
            instance = this;

        gameaActionMap = new();
        playerActions = gameaActionMap.Player;
    }

    /// <summary>
    /// Enable Player Input
    /// </summary>
    public void EnablePlayerInput()
    {
        playerActions.Enable();
        //Some more stuffs when Input Enabled will written here
    }

    /// <summary>
    /// Disable Player Input
    /// </summary>
    public void DisablePlayerInput()
    {
        playerActions.Disable();
        //Some more stuffs when Input Disabled will written here
    }
}
