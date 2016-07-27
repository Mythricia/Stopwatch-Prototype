using UnityEngine;

[SelectionBase]
public class BasicRangeTarget : RangeTarget
{
    private RangeController myController;
    private TargetHint[] myHints;

    public GameObject hitPrefab;


    public override void Enable()
    {
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;

        if (myHints != null)
        {
            foreach (var hint in myHints)
            {
                hint.Activate();
            }
        }
    }


    public override void Setup(RangeController newController)
    {
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        myController = newController;
        myHints = transform.parent.GetComponentsInChildren<TargetHint>();
    }


    public override bool TryHit(GameObject other)
    {

        if (hitPrefab != null)
            Instantiate(hitPrefab, transform.position, transform.rotation);

        WasHit();
        return true;
    }


    protected override void WasHit()
    {
        if (myHints != null)
        {
            foreach (var hint in myHints)
            {
                hint.Deactivate();
            }
        }

        myController.TargetHit(this);
    }


    public override void Disable()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
    }
}