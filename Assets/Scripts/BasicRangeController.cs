using UnityEngine;
using System.Collections.Generic;


class BasicRangeController : RangeController
{
    private List<RangeTarget> myTargets = new List<RangeTarget>();
    private List<BasicRangeTrigger> myTriggers = new List<BasicRangeTrigger>();

    private bool hasInitialized = false;

    private int numTargets;


    protected override void Start()
    {
        if (Initialize() == false)
        {
            Debug.LogWarning(this.GetType().Name
            + " attached to " + gameObject.name
            + " at "
            + gameObject.transform.position.ToString()
            + " failed to initialize!!");

            return;
        }

    }


    protected override void Update()
    {
        if (!hasInitialized || !isActive)
            return;

        if (isActive && myTargets.Count <= 0)
            Disable();
    }


    public override void Enable()
    {
        foreach (var tar in myTargets)
        {
            tar.Enable();
        }

        foreach (var trig in myTriggers)
        {
            trig.Trigger();
        }


        isActive = true;

        RangeGameManager.rgm.StartTimer();
    }


    public override void Disable()
    {
        RangeGameManager.rgm.RangeCompleted();
        RangeGameManager.rgm.StopTimer();

        Debug.Log("Range #" + GetInstanceID() + " has shut down.");

        foreach (var trig in myTriggers)
        {
            trig.Sleep();
        }

        isActive = false;
    }


    public override void TargetHit(RangeTarget target)
    {
        target.Disable();

        myTargets.Remove(target);
    }


    protected override bool Initialize()
    {
        myTargets.AddRange(GetComponentsInChildren<BasicRangeTarget>());
        myTriggers.AddRange(GetComponentsInChildren<BasicRangeTrigger>());

        if ((myTargets.Count == 0) || (myTriggers.Count == 0))
            return false;


        foreach (var tar in myTargets)
        {
            tar.Setup(this);
        }

        foreach (var trig in myTriggers)
        {
            trig.Setup(this);
        }


        hasInitialized = true;
        return true;
    }
}