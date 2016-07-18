using UnityEngine;

[SelectionBase]
public class RangeTarget : MonoBehaviour
{
    private RangeController myController;
    public TargetHint[] myHints;
    [Space(10)]
    public GameObject hitPrefab;
    // Add explosion prefab

    public void enableTarget(bool active)
    {
        gameObject.SetActive(active);

        if (myHints != null)
        {
            foreach (var hint in myHints)
            {
                hint.Activate();
            }
        }
    }


    public void Setup(bool active, RangeController newController)
    {
        gameObject.SetActive(active);
        myController = newController;
    }


    void OnCollisionEnter(Collision other)
    {
        if (hitPrefab != null)
            Instantiate(hitPrefab, transform.position, transform.rotation);

        // only do stuff if hit by a projectile
        if (other.gameObject.tag == "Projectile")
        {
            Die();
        }
    }


    private void Die()
    {
        if (!myController)
            return;

        if (myHints != null)
        {
            foreach (var hint in myHints)
            {
                hint.Deactivate();
            }
        }

        myController.TargetDied(this);
    }
}