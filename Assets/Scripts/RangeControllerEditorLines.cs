using UnityEngine;
using System.Collections.Generic;


[ExecuteInEditMode]
public class RangeControllerEditorLines : MonoBehaviour
{
    private List<BasicRangeTarget> rangeTargets = new List<BasicRangeTarget>();
    

    void Update()
    {
        rangeTargets.AddRange(GetComponentsInChildren<BasicRangeTarget>());


        if (rangeTargets.Count != 0)
        {
            foreach (var target in rangeTargets)
            {
                if (target == null)
                    return;

                Debug.DrawLine(gameObject.transform.position, target.transform.position, Color.red);
            }
        }
    }
}