using UnityEngine;

public class Weapon_BasicProjectile : Weapon
{
    public GameObject projectile;   // Projectile prefab that the player shoots
    public float power = 200.0f;    // Force of said projectile
    Transform playerCameraTransform;


    // Reference to AudioClip to play
    public AudioClip shootSFX;
    private AudioSource myAudioSource;
    public float sfxVolume = 0.75f;

    public float fireRate = 0.15f;
    private float lastFired = 0f;
    public Vector3 projectileOriginOffset = new Vector3(0,-0.2f,0);


    public override void FireWeapon()
    {
        if (projectile != null && IsReady())
        {
            // Instantiante projectile at the camera + 1 meter forward with camera rotation
            GameObject newProjectile = Instantiate(projectile, playerCameraTransform.position + projectileOriginOffset + playerCameraTransform.forward,
                                                   playerCameraTransform.rotation) as GameObject;

            newProjectile.GetComponent<Rigidbody>().AddForce(playerCameraTransform.forward * power, ForceMode.VelocityChange);

            lastFired = Time.time;

            // play sound effect if set
            if (shootSFX)
                myAudioSource.PlayOneShot(shootSFX, sfxVolume);
        }
    }


    public override bool IsReady()
    {
        return (Time.time - lastFired >= fireRate);
    }


    public override void Initialize()
    {
        myAudioSource = GetComponent<AudioSource>();
        playerCameraTransform = Camera.main.transform;
    }
}
