using UnityEngine;
using System.Collections.Generic;


public class RangeController : MonoBehaviour
{
    public List<GameObject> rangeTargets;
    public bool startActive = false;

    public GameObject rangeButton;

    private bool hasInitialized = false;


    void Start()
    {
        if (!hasInitialized)
        {
            InitializeTargets();
            InitializeButton();
        }

        // if(!RangeGameManager.rgm)
        // throw new Exception("No RangeGameManager in the scene!!");
    }


    void Update()
    {
        if (rangeTargets.Count == 0)
        {
            RangeGameManager.rgm.RangeCompleted();
            RangeGameManager.rgm.StopTimer();
            Debug.Log("Range #" + GetInstanceID() + " has shut down.");
            rangeButton.GetComponent<ConsoleButton>().Sleep();
            gameObject.SetActive(false);
        }
    }



    public void ButtonPressed()
    {
        for (int i = 0; i < rangeTargets.Count; i++)
        {
            rangeTargets[i].GetComponent<RangeTarget>().enableTarget(true);
        }

        RangeGameManager.rgm.StartTimer();
    }




    void InitializeButton()
    {
        if (rangeButton)
            rangeButton.GetComponent<ConsoleButton>().Setup(gameObject);
    }


    void InitializeTargets()
    {
        hasInitialized = true;

        if (rangeTargets.Count == 0)
        {
            return;
        }


        for (int i = 0; i < rangeTargets.Count; i++)
        {
            rangeTargets[i].GetComponent<RangeTarget>().Setup(startActive, gameObject);
        }
    }


    public void TargetDied(GameObject target)
    {
        rangeTargets.Remove(target);
        Destroy(target);
    }
}