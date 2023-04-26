using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpForce = 10f;
    public float mouseSensitivity = 100f;
    public float sprintSensitivityMultiplier = 2f;
    public float cameraSmoothing = 0.1f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody rb;
    private Camera playerCamera;

    private float xRotation = 0f;
    private float currentSpeed = 0f;

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Get the horizontal and vertical input axes
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Check if the player is sprinting
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        // Calculate the movement direction based on the input axes and the player's forward and right directions
        Vector3 movementDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        movementDirection = movementDirection.normalized;

        // Calculate the current movement speed based on whether the player is sprinting or not
        currentSpeed = isSprinting ? sprintSpeed : walkSpeed;

        // Apply the movement direction to the player's Rigidbody while preserving the y component of the velocity
        rb.velocity = new Vector3(movementDirection.x * currentSpeed, rb.velocity.y, movementDirection.z * currentSpeed);

        // Get the mouse input axes and apply
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.Rotate(Vector3.up * mouseX);

        // Calculate the current camera sensitivity based on whether the player is sprinting or not
        float currentSensitivity = isSprinting ? mouseSensitivity * sprintSensitivityMultiplier : mouseSensitivity;

        // Smoothly rotate the camera using Lerp1
        Quaternion targetRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerCamera.transform.localRotation = Quaternion.Lerp(playerCamera.transform.localRotation, targetRotation, cameraSmoothing);

        // Adjust field of view based on whether the player is sprinting or not
        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, isSprinting ? 70f : 60f, cameraSmoothing);
    }
}




