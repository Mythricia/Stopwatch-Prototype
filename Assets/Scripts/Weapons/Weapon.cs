using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract bool IsReady();

    public abstract void FireWeapon();

    public abstract void Initialize();
}