using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;
    private Rigidbody rb;
    private bool isGrounded = true;

    private float horizontalInput;
    private float verticalInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Taking input from horizontal and vertical axes and usign it to move player
    // Updating the rotation of player according to the direction of movement
    private void Update()
    {
        horizontalInput = InputManager.GetInstance.MoveInput.x;
        verticalInput = InputManager.GetInstance.MoveInput.y;

        Vector3 movementDirection = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;

        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10.0f);
        }

        // Handling jumping
        if (isGrounded && InputManager.GetInstance.IsJumpPressed)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        // Apply movement force to the Rigidbody
        rb.velocity = new Vector3(horizontalInput * moveSpeed, rb.velocity.y, verticalInput * moveSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG)) // Change "Ground" to your ground's tag
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG)) // Change "Ground" to your ground's tag
        {
            isGrounded = false;
        }
    }


    #region  Cached Properties

    private readonly static string GROUND_TAG = "Ground";

    #endregion
}
