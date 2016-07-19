using UnityEngine;

[SelectionBase]
public class BasicRangeTarget : MonoBehaviour
{
    private BasicRangeController myController;
    private TargetHint[] myHints;

    public GameObject hitPrefab;


    public void Enable()
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


    public void Setup(BasicRangeController newController)
    {
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        myController = newController;
        myHints = transform.parent.GetComponentsInChildren<TargetHint>();
    }


    void OnCollisionEnter(Collision other)
    {
        if (hitPrefab != null)
            Instantiate(hitPrefab, transform.position, transform.rotation);

        if (other.gameObject.tag == "Projectile")
        {
            WasHit();
        }
    }


    void WasHit()
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


    public void Disable()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
    }
}