using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 720f; // Speed at which the player rotates
    public float gravity = -9.81f;

    private CharacterController controller;
    private Transform cameraTransform;

    private float verticalVelocity;
    private Vector2 moveInput;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Ensure the cameraTransform is assigned
        cameraTransform = Camera.main != null ? Camera.main.transform : null;
        if (cameraTransform == null)
        {
            Debug.LogError("Main Camera not found! Make sure your camera is tagged as 'MainCamera'.");
        }
    }

    void Update()
    {
        if (cameraTransform != null)
        {
            MovePlayer();
        }
        else
        {
            Debug.LogWarning("Skipping player movement because cameraTransform is not assigned.");
        }
    }

    private void MovePlayer()
    {
        // Get the direction relative to the camera
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Flatten the direction vectors on the y-axis
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Calculate the movement direction
        Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;

        // Apply gravity
        if (controller.isGrounded)
        {
            verticalVelocity = -1f; // Small downward force to keep the player grounded
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        // Rotate the player to face the movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Final movement vector
        Vector3 move = moveDirection * speed + Vector3.up * verticalVelocity;
        controller.Move(move * Time.deltaTime);
    }

    public void MoveStarted(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void MoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }
}
