using UnityEngine;

[SelectionBase]
public class ConsoleButton : MonoBehaviour
{
    private AudioSource audioSource;
    private Transform player;
    private float distance;

    private Vector3 recoveryMarkerPosition; // Reference for the player recovery in-editor location marker
    private Renderer recoveryIndicator;     // The button console recovery indicator
                                            // NOTE: Maybe make the recovery indicator itself configurable in editor? Easy change, not sure if necessary.
    private bool isCurrentRecoveryPosition; // Is this console the current recovery position?
    private bool markerExists = false;      // Does the in-editor recovery marker exist?

    public float triggerDist = 2.0f;        // Distance at which the console button highlights
    public float triggerCooldown = 1f;
    public bool repeatTriggerable = false;

    public Color32 currentColor = new Color32(200, 100, 215, 255);      // If this console is the current recovery position
    public Color32 notCurrentColor = new Color32(25, 25, 25, 255);      // If this consle is NOT the current recovery position

    public AudioClip setRecoveryClip;   // Activated this console recovery location

    // Colours for the button itself - default, close (highlight), disabled
    private Renderer rend;

    private Color32 defaultColor;   // Defaults to the actual material color
    private Color32 closeColor;
    private Color32 sleepColor = new Color32(25, 25, 25, 255);


    private RangeController myRangeController;
    private bool interactable = true;


    void Start()
    {
        // Find the in-editor recovery marker if it exists
        if (transform.Find("RecoveryMarker") != null)
        {
            recoveryMarkerPosition = transform.Find("RecoveryMarker").gameObject.transform.position;
            markerExists = true;
        }

        // Find the player if it exists
        if (GameObject.FindWithTag("Player") != null)
        {
            player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }

        // Find the console recovery indicator child object
        if (transform.Find("RecoveryIndicator") != null)
        {
            recoveryIndicator = transform.Find("RecoveryIndicator").gameObject.GetComponent<Renderer>();
        }

        rend = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();

        defaultColor = rend.material.color;
        closeColor = new Color32(39, 143, 14, 255);
    }


    void Update()
    {
        if (player == null)
            return;

        checkIfCurrent();   // Check if this is the currently active player recovery point

        if (isCurrentRecoveryPosition)
            recoveryIndicator.material.color = currentColor;
        else
            recoveryIndicator.material.color = notCurrentColor;

        if (!interactable)
            return;

        distance = Vector3.Distance(transform.position, player.position);

        if (interactable)
        {
            if (distance <= triggerDist)
            {
                rend.material.color = closeColor;
            }
            else
            {
                rend.material.color = defaultColor;
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        if (markerExists)
        {
            Vector3 currentRecovPosition = other.gameObject.GetComponent<PlayerController>().getRecoveryPosition();
            if (currentRecovPosition == recoveryMarkerPosition)
                return;

            other.gameObject.GetComponent<PlayerController>().setRecoveryPosition(recoveryMarkerPosition);
            audioSource.PlayOneShot(setRecoveryClip);
        }
    }


    void checkIfCurrent()
    {
        Vector3 currentRecovPosition = player.GetComponent<PlayerController>().getRecoveryPosition();
        if (currentRecovPosition == recoveryMarkerPosition)
            isCurrentRecoveryPosition = true;
        else
            isCurrentRecoveryPosition = false;
    }


    public void Setup(RangeController controller)
    {
        myRangeController = controller;
    }


    public void Interact(GameObject actor)
    {
        if (interactable)
        {
            Debug.Log("I was interacted with by " + actor);
            myRangeController.ButtonPressed();
        }
    }


    public void ButtonPressed()
    {
        interactable = false;
        rend.material.color = Color.blue;

        if (repeatTriggerable)
            Invoke("ButtonReset", triggerCooldown);
    }


    private void ButtonReset()
    {
        interactable = true;
        rend.material.color = defaultColor;
    }


    public void Sleep()
    {
        interactable = false;
        rend.material.color = sleepColor;
    }
}