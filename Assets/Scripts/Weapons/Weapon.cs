using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected static Transform playerCameraTransform;


    // Assign variable at compile time
    static Weapon()
    {
        playerCameraTransform = Camera.main.transform;
    }


    public abstract bool IsReady();

    public abstract void FireWeapon();

    public abstract void Initialize();
}