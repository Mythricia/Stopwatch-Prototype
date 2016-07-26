using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected Transform playerCameraTransform;

    void Awake()
    {
        playerCameraTransform = Camera.main.transform;
    }

    public abstract bool IsReady();

    public abstract void FireWeapon();

    public abstract void Initialize();
}