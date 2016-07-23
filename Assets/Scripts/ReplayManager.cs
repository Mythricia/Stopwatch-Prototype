using UnityEngine;
using System.Collections.Generic;

public class ReplayManager : MonoBehaviour
{
    public GUIStyle labelStyle;

    bool isRecording = false;
    bool isPlaying = false;

    int index = 0;

    public float replayRate = 1 / 30;
	
    float lastFrame = 0f;

    Transform tPlayer;
    Transform tCamera;

    List<Vector3[]> samples = new List<Vector3[]>(10000);
    Vector3[][] storedReplay;

    Vector3[] savedPosition = new Vector3[2];


    void Start()
    {
        tPlayer = GameObject.FindWithTag("Player").transform;
        tCamera = Camera.main.transform;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5) && !isPlaying)
        {
            if (isRecording)
                stopRecording();
            else
                startRecording();

            return;
        }

        if (Input.GetKeyDown(KeyCode.F8))
        {
            if (!isPlaying)
                startPlaying();
            else if (isPlaying)
                stopPlaying();
        }

        if (isPlaying)
        {
            playNextFrame();
        }

        if (isRecording && (Time.time - lastFrame >= replayRate))
        {
            recordSample();
            lastFrame = Time.time;
        }
    }


    void startRecording()
    {
        if (isRecording)
            return;

        isRecording = true;
        samples.Clear();
        lastFrame = Time.time;
    }


    void stopRecording()
    {
        if (!isRecording)
            return;

        isRecording = false;

        storedReplay = samples.ToArray();
        samples.Clear();

		string s = "Total replay size in bytes: " + sizeof(float) * storedReplay.Length * storedReplay[0].Length * 4;
		Debug.Log(s);
    }


    void startPlaying()
    {
        if (storedReplay == null)
            return;

        if (storedReplay.Length == 0)
        {
            Debug.Log("Replay was of length 0 !!");
            return;
        }
        else if (isRecording)
        {
            Debug.Log("Trying to replay while recording!!");
            return;
        }

        savedPosition[0] = tPlayer.position;
        savedPosition[1] = tCamera.eulerAngles;


        index = 0;
        isPlaying = true;

        tPlayer.GetComponent<CharacterController>().enabled = false;
        tPlayer.GetComponent<MouseLooker>().enabled = false;
    }


    void stopPlaying()
    {
        index = 0;
        isPlaying = false;

        tPlayer.position = savedPosition[0];
        tCamera.eulerAngles = savedPosition[1];

        tPlayer.GetComponent<CharacterController>().enabled = true;
        tPlayer.GetComponent<MouseLooker>().enabled = true;
    }


    void recordSample()
    {
        samples.Add(new Vector3[] { tPlayer.position, tCamera.eulerAngles });
    }


    void playNextFrame()
    {
        if (index < storedReplay.Length)
        {
            if (Time.time - lastFrame >= replayRate)
            {
                tPlayer.position = storedReplay[index][0];
                tCamera.eulerAngles = storedReplay[index][1];
                index++;
                lastFrame = Time.time;
            }
        }
        else
        {
            stopPlaying();
        }
    }


    void OnGUI()
    {
        if (isRecording)
        {
            GUI.Label(new Rect(10, 50, 100, 20), "Recorded " + samples.Count.ToString() + " samples");
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2 + 100, 0, 20), "RECORDING REPLAY", labelStyle);
        }
        else if (isPlaying)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2 + 100, 0, 20), "! REPLAY !", labelStyle);
        }
    }
}
