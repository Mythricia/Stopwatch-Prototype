using UnityEngine;

public class TargetHint : MonoBehaviour
{
    public Color32 disabledColor = new Color32(25, 25, 25, 255);
    public Color32 enabledColor = new Color32(240, 240, 0, 255);

    [Tooltip("Overrides the Material color for idle state.")]
    public bool overrideIdleColor = false;
    public Color32 idleColor = new Color32(15, 100, 15, 255);
    // Overrides the Material color for idle state

    private Renderer myRenderer;


    void Start()
    {
        myRenderer = GetComponent<Renderer>();

        if (overrideIdleColor == false)
            idleColor = myRenderer.material.color;

        Idle();
    }


    public void Activate()
    {
        myRenderer.material.color = enabledColor;
    }


    public void Deactivate()
    {
        myRenderer.material.color = disabledColor;
    }


    public void Idle()
    {
        myRenderer.material.color = idleColor;
    }
}
