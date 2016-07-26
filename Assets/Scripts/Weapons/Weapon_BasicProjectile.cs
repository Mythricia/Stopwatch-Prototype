using UnityEngine;

public class Weapon_BasicProjectile : Weapon
{
    public GameObject projectile;   // Projectile prefab that the player shoots
    public float power = 200.0f;    // Force of said projectile


    // Reference to AudioClip to play
    public AudioClip shootSFX;
    public float sfxVolume = 1.0f;

    public float fireRate = 0.15f;
    public float lastFired = 0f;


    public override void FireWeapon()
    {
        if (projectile != null && IsReady())
        {
            // Instantiante projectile at the camera + 1 meter forward with camera rotation
            GameObject newProjectile = Instantiate(projectile, playerCameraTransform.position + playerCameraTransform.forward,
                                                   playerCameraTransform.rotation) as GameObject;

            newProjectile.GetComponent<Rigidbody>().AddForce(playerCameraTransform.forward * power, ForceMode.VelocityChange);

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


    public override bool IsReady()
    {
        return (Time.time - lastFired >= fireRate);
    }


    public override void Initialize()
    {
    }
}
