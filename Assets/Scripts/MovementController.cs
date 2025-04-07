using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 5f; // Speed of movement
    public float rotationSpeed = 150f; // Rotation sensitivity
    public float jumpForce = 7f; // Strength of jump
    public float edgeRotationSpeed = 50f; // Rotation speed when mouse is at screen edge
    public float screenEdgeThreshold = 10f; // Distance in pixels from screen edges for rotation

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        //WARNING TO DO UNLOCK AND UNHIDE THE MOUSE
        Cursor.lockState = CursorLockMode.Locked;
        // Hide the cursor
        Cursor.visible = false;


        // Get Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Ensure the Rigidbody doesn't rotate due to physics collisions
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Handle mouse-based rotation
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX * rotationSpeed * Time.deltaTime, 0);

        // Handle continuous rotation when the mouse is near screen edges
        Vector3 mousePosition = Input.mousePosition;
        if (mousePosition.x <= screenEdgeThreshold)
        {
            transform.Rotate(0, -edgeRotationSpeed * Time.deltaTime, 0); // Rotate left
        }
        else if (mousePosition.x >= Screen.width - screenEdgeThreshold)
        {
            transform.Rotate(0, edgeRotationSpeed * Time.deltaTime, 0); // Rotate right
        }

        // Handle jumping with spacebar
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        // Handle movement with arrow keys
        float horizontal = Input.GetAxis("Horizontal"); // Left/right
        float vertical = Input.GetAxis("Vertical");     // Forward/backward

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            horizontal = horizontal * 1.5f;
            vertical = vertical * 1.5f;
        }
        // Calculate movement direction relative to the player's orientation
        Vector3 movement = transform.forward * vertical + transform.right * horizontal;

        // Apply movement to the Rigidbody
        rb.linearVelocity = new Vector3(movement.x * movementSpeed, rb.linearVelocity.y, movement.z * movementSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if grounded when colliding with a surface
        if (collision.contacts.Length > 0 && collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}