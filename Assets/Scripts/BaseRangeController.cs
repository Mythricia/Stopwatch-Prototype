using UnityEngine;
using System.Collections.Generic;


public class BaseRangeController : MonoBehaviour
{
    private List<BaseRangeTarget> targets = new List<BaseRangeTarget>();
    private List<BaseRangeTrigger> triggers;

    private bool hasInitialized = false;
    private bool isActive = false;



    void Start()
    {
        if (Initialize() == false)
            return;

    }


    void Update()
    {

    }


    bool Initialize()
    {
        targets.AddRange(GetComponentsInChildren<BaseRangeTarget>());
        triggers.AddRange(GetComponentsInChildren<BaseRangeTrigger>());

        if (targets.Count == 0)
            return false;

        if (triggers.Count == 0)
            return false;

        hasInitialized = true;
        return true;
    }


    void Trigger()
    {

    }


    void Finished()
    {

    }
}