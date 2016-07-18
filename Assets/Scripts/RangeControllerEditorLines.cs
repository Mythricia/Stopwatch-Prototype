using UnityEngine;
using System.Collections.Generic;


[ExecuteInEditMode]
public class RangeControllerEditorLines : MonoBehaviour
{
    private List<RangeTarget> rangeTargets;


    void Start()
    {
        rangeTargets = gameObject.GetComponent<RangeController>().rangeTargets;
    }


    void Update()
    {
        if (rangeTargets.Count != 0)
        {
            foreach (var target in rangeTargets)
            {
                Debug.DrawLine(gameObject.transform.position, target.transform.position, Color.red);
            }
        }
    }
}