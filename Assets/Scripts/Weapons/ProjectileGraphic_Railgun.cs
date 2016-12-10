using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class ProjectileGraphic_Railgun : MonoBehaviour
{
    public float beamWidth = 0.1f;
    public float beamLifeTime = 1f;
    public Vector3 projectileOriginOffset = new Vector3(0, -0.2f, 0);


    private LineRenderer lr;
    private float creationTime;



    void Update()
    {
        if (Time.time - creationTime >= beamLifeTime)
            Destroy(gameObject);

        Color newColor = lr.material.color;
        newColor.r = Mathf.Lerp(1, 0, (Time.time - creationTime));
        newColor.b = Mathf.Lerp(1, 0, (Time.time - creationTime));

        lr.material.color = newColor;
    }

    public void Initialize(Vector3 origin, Vector3 destination)
    {
        origin += projectileOriginOffset;
        lr = GetComponent<LineRenderer>();
        creationTime = Time.time;

        lr.startWidth = beamWidth;
        lr.endWidth = beamWidth;
        lr.numPositions = 2;

        lr.SetPositions(new Vector3[] { origin, destination });
    }
}
