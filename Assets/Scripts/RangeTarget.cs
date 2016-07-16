using UnityEngine;

[SelectionBase]
public class RangeTarget : MonoBehaviour
{
    private GameObject controller;
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


    public void Setup(bool active, GameObject newController)
    {
        gameObject.SetActive(active);
        controller = newController;
    }


    private void Die()
    {
        if (!controller)
            return;

        if (myHints != null)
        {
            foreach (var hint in myHints)
            {
                hint.Deactivate();
            }
        }

        controller.GetComponent<RangeController>().TargetDied(gameObject);
    }


    void OnCollisionEnter(Collision newCollision)
    {
        if (hitPrefab != null)
            Instantiate(hitPrefab, transform.position, transform.rotation);

        // only do stuff if hit by a projectile
        if (newCollision.gameObject.tag == "Projectile")
        {
            Die();
        }
    }
}