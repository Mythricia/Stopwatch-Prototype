using UnityEngine;
using System.Collections.Generic;


[ExecuteInEditMode]
public class RangeControllerEditorLines : MonoBehaviour
{
    private List<GameObject> rangeTargets;


    void Start()
    {
        rangeTargets = gameObject.GetComponent<RangeController>().rangeTargets;
    }


    void Update()
    {
        if (rangeTargets.Count != 0)
        {
            foreach (var tar in rangeTargets)
            {
                Debug.DrawLine(gameObject.transform.position, tar.gameObject.transform.position, Color.red);
            }
        }
    }
}