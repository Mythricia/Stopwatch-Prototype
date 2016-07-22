using UnityEngine;


public abstract class RangeTrigger : MonoBehaviour
{
    RangeController myRangeController;

    protected abstract void Start();
    protected abstract void Update();

    public abstract void Setup(RangeController controller);

    public abstract void Trigger();
    public abstract void Sleep();
}