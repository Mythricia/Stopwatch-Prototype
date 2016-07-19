using UnityEngine;
using UnityEngine.UI;

public class ShootingController : MonoBehaviour
{
    public GameObject projectile;   // Projectile prefab that the player shoots
    public float power = 200.0f;    // Force of said projectile

    // Reference to AudioClip to play
    public AudioClip shootSFX;
    public float sfxVolume = 1.0f;

    // interaction stuff
    private RaycastHit hit;
    public float interactRange = 2.0f;
    private int layerMask = 1 << 8;     // Only interact with objects that are part of the custom "Interactable" Layer. (0000 0001) << 8 = (1 0000 0000)  -> Only interact with the 8th layer

    public float fireRate = 0.15f;
    private float lastFired = 0f;

    // Debug options
    public Toggle bounceToggle;
    public Toggle rateToggle;

    private string userPrefBounce = "LimitBounces";
    private string userPrefFireRate = "LimitFireRate";


    void Start()
    {
        if (PlayerPrefs.GetInt(userPrefBounce) == 1)
            bounceToggle.isOn = true;
        else
            bounceToggle.isOn = false;


        if (PlayerPrefs.GetInt(userPrefFireRate) == 1)
            rateToggle.isOn = true;
        else
            rateToggle.isOn = false;
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1") && MouseLooker.doRotation)
        {
            if (projectile)
            {
                // Bail immediately if we're using fire rate limiting and the CD is not expired
                if ((Time.time - lastFired) < fireRate && rateToggle.isOn == true)
                    return;

                // Instantiante projectile at the camera + 1 meter forward with camera rotation
                GameObject newProjectile = Instantiate(projectile, transform.position + transform.forward, transform.rotation) as GameObject;
                newProjectile.GetComponent<ProjectileDestructor>().setBounces(bounceToggle.isOn);

                newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * power, ForceMode.VelocityChange);

                lastFired = Time.time;

                // play sound effect if set
                if (shootSFX)
                {
                    if (newProjectile.GetComponent<AudioSource>())
                    { // the projectile has an AudioSource component
                      // play the sound clip through the AudioSource component on the gameobject.
                      // note: The audio will travel with the gameobject.
                        newProjectile.GetComponent<AudioSource>().PlayOneShot(shootSFX, sfxVolume);
                    }
                    else
                    {
                        // dynamically create a new gameObject with an AudioSource
                        // this automatically destroys itself once the audio is done
                        AudioSource.PlayClipAtPoint(shootSFX, newProjectile.transform.position, sfxVolume);
                    }
                }
            }
        }


        if (Input.GetButtonDown("Interact"))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, interactRange, layerMask))
            {
                if (hit.collider.gameObject.GetComponent<BasicRangeTrigger>() != null)
                {
                    hit.collider.gameObject.GetComponent<BasicRangeTrigger>().Interact(gameObject);
                }
            }
        }
    } // End of Update()


    public void toggleLimitBounces()
    {
        int prefValue = bounceToggle.isOn ? 1 : 0;

        PlayerPrefs.SetInt(userPrefBounce, prefValue);
        PlayerPrefs.Save();
    }


    public void toggleFireRate()
    {
        int prefValue = rateToggle.isOn ? 1 : 0;

        PlayerPrefs.SetInt(userPrefFireRate, prefValue);
        PlayerPrefs.Save();
    }
}
