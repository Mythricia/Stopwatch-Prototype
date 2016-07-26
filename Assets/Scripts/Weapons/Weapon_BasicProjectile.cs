using UnityEngine;

public class Weapon_BasicProjectile : MonoBehaviour
{

    public float timeOut = 1.0f;
    public bool detachChildren = false;
    public GameObject hitParticle;
    bool limitBounces = false;
    public int bounceLimit = 2;
    private int numBounces = 0;


    void Awake()
    {
        bounceLimit = Mathf.Clamp(bounceLimit, 0, 100);
        // invoke the DestroyNow funtion to run after timeOut seconds
        Invoke("DestroyNow", timeOut);
    }


    // destroy projectile _immediately_ if we hit a RangeTarget, else count towards bounce limit
    void OnCollisionEnter(Collision newCollision)
    {
        if (newCollision.gameObject.tag == "RangeTarget")
        {
            if (hitParticle)
            {
                Instantiate(hitParticle, transform.position, Quaternion.Euler(0, 180, 0));
            }

            Destroy(gameObject);
        }
        else if (limitBounces)
        {
            if (numBounces >= bounceLimit)
                Destroy(gameObject);

            numBounces++;
        }
    }


    public void DestroyNow()
    {
        if (detachChildren)
        { // detach the children before destroying if specified
            transform.DetachChildren();
        }

        // destory the game Object
        Destroy(gameObject);
    }


    public void setBounces(bool toggle)
    {
        limitBounces = toggle;
    }
}
