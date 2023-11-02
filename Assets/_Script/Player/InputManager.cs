using UnityEngine;

/// <summary>
/// Responsible for Reading the Input from Device.
/// Using Singleton Pattern <br></br>
///Currently Defined Movement are MoveInput(Vector2), IsJumpPressed(bool), IsCrouchedPressed(bool), IsChangeSizePressed(bool)
/// </summary>
public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    /// <summary>
    /// Return the instance of InputManager
    /// </summary>
    public static InputManager GetInstance => instance;

    private GameInputActionMap gameaActionMap;
    private GameInputActionMap.PlayerActions playerActions;

    private Vector2 moveInput;
    private bool isJumpPressed;
    private bool isCrouchPressed;
    private bool isChangeSizePressed;

    public Vector2 MoveInput => moveInput;
    public bool IsJumpPressed => isJumpPressed;
    public bool IsCrouchPressed => isCrouchPressed;
    public bool IsChangeSizePressed => isChangeSizePressed;

    private void Awake() => Intialization();
    private void OnEnable() => EnablePlayerInput();
    private void OnDisable() => DisablePlayerInput();

    private void Update()
    {
        if (!playerActions.enabled) //! if input are disable then dont read input unnecessarily
            return;

        moveInput = playerActions.Move.ReadValue<Vector2>(); 
        isJumpPressed = playerActions.Jump.WasPerformedThisFrame();
        isCrouchPressed = playerActions.Crouch.WasPerformedThisFrame();
        isChangeSizePressed = playerActions.SizeChange.WasPerformedThisFrame();
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