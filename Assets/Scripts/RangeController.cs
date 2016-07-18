using UnityEngine;
using System.Collections.Generic;


public class RangeController : MonoBehaviour
{
    public List<RangeTarget> rangeTargets;
    public bool startActive = false;

    public ConsoleButton consoleButton;

    private bool hasInitialized = false;


    void Start()
    {
        if (!hasInitialized)
        {
            Initialize();
        }
    }


    void Update()
    {
        if (rangeTargets.Count == 0)
        {
            RangeGameManager.Rgm.RangeCompleted();
            RangeGameManager.Rgm.StopTimer();
            Debug.Log("Range #" + GetInstanceID() + " has shut down.");
            consoleButton.Sleep();
            gameObject.SetActive(false);
        }
    }



    public void ButtonPressed()
    {
        for (int i = 0; i < rangeTargets.Count; i++)
        {
            rangeTargets[i].enableTarget(true);
        }

        RangeGameManager.Rgm.StartTimer();
    }


    void Initialize()
    {
        hasInitialized = true;


        if (consoleButton)
            consoleButton.Setup(gameObject);

        if (rangeTargets.Count == 0)
        {
            return;
        }


        for (int i = 0; i < rangeTargets.Count; i++)
        {
            rangeTargets[i].Setup(startActive, this);
        }
    }


    public void TargetDied(RangeTarget target)
    {
        rangeTargets.Remove(target);
        target.gameObject.SetActive(false);
    }
}