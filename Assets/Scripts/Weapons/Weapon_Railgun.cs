using UnityEngine;
using System.Linq;

public class Weapon_Railgun : Weapon
{
    Transform playerCameraTransform;

    public AudioClip shootSFX;
    public float sfxVolume = 0.75f;
    public GameObject beamPrefab;

    public float fireRate = 0.75f;
    private float lastFired = 0f;
    private float beamRange = 1000f;


    public override void FireWeapon()
    {
        if (IsReady())
        {
            Ray ray = new Ray(playerCameraTransform.position + playerCameraTransform.forward, playerCameraTransform.forward);
            Vector3 hitLocation = playerCameraTransform.forward * beamRange;

            RaycastHit[] rayHits = Physics.RaycastAll(ray, beamRange);

            rayHits = rayHits.OrderBy(h => h.distance).ToArray();


            if (rayHits != null)
            {
                foreach (var hit in rayHits)
                {
                    Debug.Log("Object: " + hit.collider.gameObject.name + " with tag: " + hit.collider.tag + " at distance: " + hit.distance);
                }


                foreach (var hit in rayHits)
                {
                    if (hit.collider.tag != "RangeTarget")
                    {
                        hitLocation = hit.point;
                        break;
                    }
                    else if (hit.collider.tag == "RangeTarget")
                        hit.collider.GetComponent<RangeTarget>().TryHit(gameObject);
                }
            }


            if (beamPrefab)
            {
                GameObject beam = Instantiate(beamPrefab, playerCameraTransform.position + playerCameraTransform.forward, playerCameraTransform.rotation) as GameObject;
                beam.GetComponent<Projectile_Railgun>().Initialize(playerCameraTransform.position, hitLocation);
            }


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