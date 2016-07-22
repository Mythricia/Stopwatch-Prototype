using UnityEngine;


public abstract class RangeTarget : MonoBehaviour
{
    public abstract void Setup(RangeController newController);

    public abstract void OnCollisionEnter(Collision other);
    protected abstract void WasHit();

    public abstract void Enable();
    public abstract void Disable();
}