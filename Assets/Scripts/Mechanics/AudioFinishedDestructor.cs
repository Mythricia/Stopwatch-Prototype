using UnityEngine;

public class AudioFinishedDestructor : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, GetComponent<AudioSource>().clip.length);
    }
}