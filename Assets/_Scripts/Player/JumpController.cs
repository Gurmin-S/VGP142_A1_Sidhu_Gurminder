using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Ensure this is included

[RequireComponent(typeof(CharacterController))]
public class JumpController : MonoBehaviour
{
    private CharacterController cc;

    public float jumpHeight = 3.0f;
    public float gravity = -9.81f;
    private float verticalVelocity;
    private bool jumpRequested;

    private Inputs inputActions; // Private instance of Inputs

    void Awake()
    {
        // Instantiate the inputActions
        inputActions = new Inputs();
    }

    void OnEnable()
    {
        // Enable the input actions and bind the jump action
        inputActions.controls.Enable();
        inputActions.controls.Jump.performed += OnJump;
    }

    void OnDisable()
    {
        // Disable the input actions and unbind the jump action
        inputActions.controls.Jump.performed -= OnJump;
        inputActions.controls.Disable();
    }

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if a jump is requested
        if (jumpRequested && cc.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpRequested = false; // Reset jump request
            Debug.Log("Jump executed");
        }

        // Apply gravity
        if (cc.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // A small negative value to keep the character grounded
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 move = new Vector3(0, verticalVelocity, 0);
        cc.Move(move * Time.deltaTime);
    }

    void OnJump(InputAction.CallbackContext context)
    {
        jumpRequested = true; // Set jumpRequested to true when the jump action is performed
    }
}
