using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // public variables
    public float moveSpeed = 5.0f;
    public float sprintSpeed = 9.0f;
    public float gravity = 9.81f;

    Vector3 recoveryPosition;

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


        if (Input.GetButton("Sprint")) // Sprint if Left Shift is held down
        {
            // Determine how much should move in the z-direction
            Vector3 movementZ = Input.GetAxis("Vertical") * Vector3.forward * sprintSpeed * Time.deltaTime;

            // Determine how much should move in the x-direction
            Vector3 movementX = Input.GetAxis("Horizontal") * Vector3.right * sprintSpeed * Time.deltaTime;

            // Convert combined Vector3 from local space to world space based on the position of the current gameobject (player)
            Vector3 movement = transform.TransformDirection(movementZ + movementX);

            // Apply gravity (so the object will fall if not grounded)
            movement.y -= gravity * Time.deltaTime;

            // Actually move the character controller in the movement direction
            myController.Move(movement);
        }
        else
        {
            // Determine how much should move in the z-direction
            Vector3 movementZ = Input.GetAxis("Vertical") * Vector3.forward * moveSpeed * Time.deltaTime;

            // Determine how much should move in the x-direction
            Vector3 movementX = Input.GetAxis("Horizontal") * Vector3.right * moveSpeed * Time.deltaTime;

            // Convert combined Vector3 from local space to world space based on the position of the current gameobject (player)
            Vector3 movement = transform.TransformDirection(movementZ + movementX);

            // Apply gravity (so the object will fall if not grounded)
            movement.y -= gravity * Time.deltaTime;

            // Actually move the character controller in the movement direction
            myController.Move(movement);
        }
    }


    public void setRecoveryPosition(Vector3 pos)
    {
        recoveryPosition = pos;
    }


    public Vector3 getRecoveryPosition()
    {
        return recoveryPosition;
    }
}
