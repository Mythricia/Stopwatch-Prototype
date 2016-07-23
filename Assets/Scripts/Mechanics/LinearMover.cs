using UnityEngine;

public class LinearMover : MonoBehaviour
{

    // Boilerplate and EditorInspector vars
    bool posX = true;
    bool posY = true;
    bool posZ = true;

    [Header("All movement in !world! coordinates")]

    public bool moveInX;
    public float xDistance = 1f;
    public float xSpeed = 1f;

    public bool moveInY;
    public float yDistance = 1f;
    public float ySpeed = 1f;

    public bool moveInZ;
    public float zDistance = 1f;
    public float zSpeed = 1f;

    // Initial position
    Vector3 startPosition;


    void Start()
    {
        startPosition = transform.position;

        // Sanitize values to only accept positive Distance and Speed values.
        // NOTE: Make this not matter at some point in the future, by not being bad at math

        xDistance = Mathf.Abs(xDistance);
        yDistance = Mathf.Abs(yDistance);
        zDistance = Mathf.Abs(zDistance);

        xSpeed = Mathf.Abs(xSpeed);
        ySpeed = Mathf.Abs(ySpeed);
        zSpeed = Mathf.Abs(zSpeed);
    }


    void FixedUpdate()
    {
        if (moveInX)
        {
            if (posX)
            {
                if ((transform.position.x - startPosition.x) < xDistance)
                    transform.Translate(Vector3.right * xSpeed * Time.fixedDeltaTime, Space.World);
                else
                    posX = !posX;
            }
            else if (!posX)
            {
                if ((transform.position.x - startPosition.x) > 0 - xDistance)
                    transform.Translate(Vector3.right * -xSpeed * Time.fixedDeltaTime, Space.World);
                else
                    posX = !posX;
            }
        }


        if (moveInY)
        {
            if (posY)
            {
                if ((transform.position.y - startPosition.y) < yDistance)
                    transform.Translate(Vector3.up * ySpeed * Time.fixedDeltaTime, Space.World);
                else
                    posY = !posY;
            }
            else if (!posY)
            {
                if ((transform.position.y - startPosition.y) > 0 - yDistance)
                    transform.Translate(Vector3.up * -ySpeed * Time.fixedDeltaTime, Space.World);
                else
                    posY = !posY;
            }
        }
        // FIXME: Combine the posY = !posY check, and perform the same fix on equivalent x / z functions


        if (moveInZ)
        {
            if (posZ)
            {
                if ((transform.position.z - startPosition.z) < zDistance)
                    transform.Translate(Vector3.forward * zSpeed * Time.fixedDeltaTime, Space.World);
                else
                    posZ = !posZ;
            }
            else if (!posZ)
            {
                if ((transform.position.z - startPosition.z) > 0 - zDistance)
                    transform.Translate(Vector3.forward * -zSpeed * Time.fixedDeltaTime, Space.World);
                else
                    posZ = !posZ;
            }
        }
    } // End of FixedUpdate()
}
