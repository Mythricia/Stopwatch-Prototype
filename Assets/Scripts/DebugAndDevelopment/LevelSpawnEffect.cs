using UnityEngine;

public class LevelSpawnEffect : MonoBehaviour
{
    Renderer[] rendArray;
    public float delayBetweenPop = 0.1f;
    int progressIndex = 0;


    void Start()
    {
        // rendArray = GetComponentsInChildren<Renderer>();
        rendArray = GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in rendArray)
        {
            rend.enabled = false;
        }

        shuffle(rendArray);

        InvokeRepeating("EnableNextRenderer", 0f, delayBetweenPop);
    }


    void EnableNextRenderer()
    {
        if (progressIndex < rendArray.Length)
        {
            rendArray[progressIndex].enabled = true;
            progressIndex++;
        }
        else
        {
            CancelInvoke();
        }
    }


    void shuffle(object[] items)
    {
        for (int t = 0; t < items.Length; t++)
        {
            object tmp = items[t];
            int r = Random.Range(t, items.Length);
            items[t] = items[r];
            items[r] = tmp;
        }
    }
}