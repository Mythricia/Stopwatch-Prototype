using UnityEngine;

public class RangeControllerEditorLines : MonoBehaviour
{
    void OnDrawGizmos()
    {
        BasicRangeTarget[] rangeTargets = GetComponentsInChildren<BasicRangeTarget>();


        if (rangeTargets != null)
        {
            Gizmos.color = Color.red;

            foreach (var target in rangeTargets)
            {
                Gizmos.DrawLine(gameObject.transform.position, target.transform.position);
            }
        }
    }
}