using UnityEngine;


public abstract class RangeController : MonoBehaviour
{
    protected bool isActive = false;


    public abstract void Start();
    public abstract void Update();


    public abstract void Enable();
    public abstract void Disable();


    protected abstract bool Initialize();

    public abstract void TargetHit(RangeTarget target);
}