using UnityEngine;

public class Debug_Speedometer : MonoBehaviour
{
    Vector3 lastPosition = Vector3.zero;

    public int numberOfSamples = 10;

    float[] distSamples;
    int index = 0;

    void Start()
    {
        distSamples = new float[numberOfSamples];

        for (int i = 0; i < distSamples.Length; i++)
        {
            distSamples[i] = 0f;
        }
    }


    void Update()
    {
        distSamples[index % distSamples.Length] = Vector3.Distance(lastPosition, transform.position);
        index++;

        lastPosition = transform.position;
    }


    void OnGUI()
    {
        float avgDist = 0;

        for (int i = 0; i < distSamples.Length; i++)
        {
            avgDist += distSamples[i];
        }

        avgDist /= distSamples.Length;

        GUI.Label(new Rect(10, 10, 100, 20), (avgDist / Time.deltaTime).ToString());
    }
}