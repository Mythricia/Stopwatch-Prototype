using UnityEngine;


class BasicRangeTrigger : RangeTrigger
{
    RangeController myController;

    public float triggerDist = 3.0f;

    private Transform player;
    private float distanceToPlayer;
    private Vector3 myPosition;

    private bool interactable = true;

    private Renderer myRenderer;

    private Color32 idleColor;  // Defaults to current object material color
    public Color32 closeColor = new Color32(39, 143, 14, 255);
    public Color32 enabledColor = new Color32(0, 0, 255, 255);
    public Color32 disabledColor = new Color32(25, 25, 25, 255);

    protected override void Start()
    {
        if (GameObject.FindWithTag("Player") != null)
            player = GameObject.FindWithTag("Player").GetComponent<Transform>();

        myRenderer = GetComponent<Renderer>();

        idleColor = myRenderer.material.color;

        myPosition = transform.position;
    }


    protected override void Update()
    {
        if (player == null || interactable == false)
            return;

        distanceToPlayer = Vector3.Distance(myPosition, player.position);

        if (distanceToPlayer <= triggerDist)
            myRenderer.material.color = closeColor;
        else
            myRenderer.material.color = idleColor;
    }



    public override void Setup(RangeController controller)
    {
        myController = controller;
    }


    public override void Trigger()
    {
        if(interactable)
        {
            myController.Enable();
        }
    }


   void ButtonPressed()
    {
        interactable = false;
        myRenderer.material.color = enabledColor;
    }


    public override void Sleep()
    {
        interactable = false;
        myRenderer.material.color = disabledColor;
    }
}