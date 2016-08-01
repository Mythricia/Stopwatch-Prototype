using UnityEngine;

public class Telepad : MonoBehaviour
{
    PlayerController myPlayer;
    bool hasPlayer = false;

    Vector3 myPosition;

    bool isCurrent = false;

    public Color inactiveColor = new Color32(15, 15, 15, 255);
    public Color activeColor = new Color32(15, 250, 15, 255);

    Renderer myPadRenderer;


    void Awake()
    {
        myPlayer = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (myPlayer != null)
            hasPlayer = true;

        myPosition = transform.position;
        myPadRenderer = transform.Find("Ring").GetComponent<Renderer>();
    }

    void OnTriggerEnter()
    {
        if (!hasPlayer || isCurrent)
            return;

        myPlayer.TelepadSet(this);
        GetComponent<AudioSource>().Play();
    }


    public Vector3 GetPosition()
    {
        return myPosition + new Vector3(0, 2.5f, 0);
    }

    public void SetCurrent(bool state)
    {
        isCurrent = state;

        if (myPadRenderer != null)
        {
            switch (state)
            {
                case false:
                    myPadRenderer.material.color = inactiveColor;
                    break;
                case true:
                    myPadRenderer.material.color = activeColor;
                    break;
            }
        }
    }
}