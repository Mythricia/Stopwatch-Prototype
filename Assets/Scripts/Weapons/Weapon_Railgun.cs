using UnityEngine;

public class Weapon_Railgun : Weapon
{
	public AudioClip shootSFX;
    public float sfxVolume = 1.0f;

    public float fireRate = 0.15f;
    private float lastFired = 0f;


    public override bool IsReady()
    {
		return false;
    }

    public override void FireWeapon()
    {
		AudioSource.PlayClipAtPoint(shootSFX, playerCameraTransform.position);
    }

    public override void Initialize()
    {

    }
}