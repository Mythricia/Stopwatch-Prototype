using UnityEngine;
using System.Collections.Generic;


[ExecuteInEditMode]
public class RangeControllerEditorLines : MonoBehaviour
{
    private List<RangeTarget> rangeTargets = new List<RangeTarget>();
    

    void Update()
    {
        rangeTargets.AddRange(GetComponentsInChildren<RangeTarget>());


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