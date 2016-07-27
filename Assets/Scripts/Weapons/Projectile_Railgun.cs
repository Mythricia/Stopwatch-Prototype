using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class Projectile_Railgun : MonoBehaviour
{
    public Color beamColorStart;
    public Color beamColorEnd;
    public float beamWidth;
    public float beamLifeTime = 1000f;

    private float lengthOfBeam;
    private LineRenderer lr;
    private float creationTime;



    void Update()
    {
        if (Time.time - creationTime >= beamLifeTime)
            Destroy(gameObject);
    }

    public void Initialize(Vector3 origin, Vector3 destination)
    {
		lr = GetComponent<LineRenderer>();
        creationTime = Time.time;

        lr.SetColors(beamColorStart, beamColorEnd);
        lr.SetWidth(beamWidth, beamWidth);
        lr.SetVertexCount(2);

        lr.SetPositions(new Vector3[] { origin, destination });
    }
}
