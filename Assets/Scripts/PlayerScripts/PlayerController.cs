﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // public variables
    public float moveSpeed = 5.0f;
    public float sprintSpeed = 9.0f;
    public float gravity = 9.81f;
    public bool canJump = false;
    public float jumpPower = 6f;
    public bool smoothMovement = true;

    Vector3 recoveryPosition;
    Telepad currentPad;

    [TooltipAttribute("Default Recovery Position")]
    public Vector3 defaultRecoveryPosition;

    CharacterController myController;


    void Start()
    {
        myController = gameObject.GetComponent<CharacterController>();
        recoveryPosition = defaultRecoveryPosition;
    }


    void Update()
    {
        if (myController.enabled != true)
            return;


        if (Input.GetButtonDown("Retry"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        if (Input.GetButtonDown("Recover"))
            transform.position = recoveryPosition;


        doMovement();
    }


    void doMovement()
    {
        Vector3 movement = new Vector3(0, 0, 0);


        // Ternary operator ? porn : ahead

        movement.x = smoothMovement ? Input.GetAxis("Horizontal") : Input.GetAxisRaw("Horizontal");
        movement.z = smoothMovement ? Input.GetAxis("Vertical") : Input.GetAxisRaw("Vertical");

        movement = transform.TransformDirection(Vector3.ClampMagnitude(movement, 1));

        movement *= (Input.GetButton("Sprint") ? sprintSpeed : moveSpeed);


        Vector3 curVelocity = myController.velocity;
        movement.y = curVelocity.y - (gravity * Time.deltaTime);

        if (canJump && Input.GetKeyDown("space") && myController.isGrounded)
            movement.y = jumpPower;


        myController.Move(movement * Time.deltaTime);
    }


    public void TelepadSet(Telepad newPad)
    {
        if (currentPad != null)
            currentPad.SetCurrent(false);

        currentPad = newPad;
        currentPad.SetCurrent(true);
        recoveryPosition = currentPad.GetPosition();
    }
}
