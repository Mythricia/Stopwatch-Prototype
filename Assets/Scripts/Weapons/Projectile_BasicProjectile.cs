using UnityEngine;

public class Projectile_BasicProjectile : MonoBehaviour
{
    public float timeOut = 5.0f;
    public GameObject hitParticle;
    public bool limitBounces = false;
    public int bounceLimit = 2;


    private int numBounces = 0;


    void Awake()
    {
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
        // destory the game Object
        Destroy(gameObject);
    }
}