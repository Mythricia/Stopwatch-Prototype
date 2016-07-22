using UnityEngine;


public abstract class RangeTrigger : MonoBehaviour
{
    public abstract void Start();
    public abstract void Update();

    public abstract void Setup(RangeController controller);

    public abstract void Trigger();
    public abstract void Sleep();
}