using UnityEngine;

public class Weapon_Railgun : Weapon
{
    Transform playerCameraTransform;

    public AudioClip shootSFX;
    public float sfxVolume = 0.75f;

    public float fireRate = 0.75f;
    private float lastFired = 0f;


    public override void FireWeapon()
    {
        if (IsReady())
        {

            lastFired = Time.time;
            AudioSource.PlayClipAtPoint(shootSFX, playerCameraTransform.position);
        }
    }


    public override bool IsReady()
    {
        return (Time.time - lastFired >= fireRate);

    }


    public override void Initialize()
    {
        playerCameraTransform = Camera.main.transform;
    }
}