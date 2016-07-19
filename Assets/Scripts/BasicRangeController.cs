using UnityEngine;
using System.Collections.Generic;


public class BasicRangeController : MonoBehaviour
{
    private List<BasicRangeTarget> myTargets = new List<BasicRangeTarget>();
    private List<BasicRangeTrigger> myTriggers;

    private bool hasInitialized = false;
    private bool isActive = false;

    private int numTargets;



    void Start()
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


    void Update()
    {
        if (!hasInitialized || !isActive)
            return;

        if (isActive && myTargets.Count <= 0)
            Disable();
    }


    void Enable()
    {
        foreach (var tar in myTargets)
        {
            tar.enableTarget(true);
        }

        foreach (var trig in myTriggers)
        {
            trig.ButtonPressed();
        }

        isActive = true;


        RangeGameManager.rgm.StartTimer();
    }


    void Disable()
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


    void TargetHit(BasicRangeTarget target)
    {
        target.Disable();

        myTargets.Remove(target);
    }


    bool Initialize()
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