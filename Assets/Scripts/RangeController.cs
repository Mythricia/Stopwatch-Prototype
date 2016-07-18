using UnityEngine;
using System.Collections.Generic;


public class RangeController : MonoBehaviour
{
    public bool startActive = false;

    private List<RangeTarget> rangeTargets = new List<RangeTarget>();
    private ConsoleButton[] consoleButtons;

    private bool hasInitialized = false;
    private bool isActive = false;


    void Start()
    {
        if (!hasInitialized)
        {
            Initialize();
        }
    }


    void Update()
    {
        if (isActive && rangeTargets.Count == 0)
        {
            RangeGameManager.rgm.RangeCompleted();
            RangeGameManager.rgm.StopTimer();
            Debug.Log("Range #" + GetInstanceID() + " has shut down.");

            foreach (var button in consoleButtons)
            {
                button.Sleep();
            }

            isActive = false;
        }
    }



    public void ButtonPressed()
    {
        for (int i = 0; i < rangeTargets.Count; i++)
        {
            rangeTargets[i].enableTarget(true);
        }

        foreach (var button in consoleButtons)
        {
            // FIXME: This is an ugly back-and-forth.. Button tells us it was pushed, and then we call it back saying hey, you were pushed.
            //        Not a huge deal, but it's really clunky and can easily be done better
            
            button.ButtonPressed();
        }

        isActive = true;

        RangeGameManager.rgm.StartTimer();
    }


    void Initialize()
    {
        hasInitialized = true;

        rangeTargets.AddRange(GetComponentsInChildren<RangeTarget>());

        consoleButtons = GetComponentsInChildren<ConsoleButton>();

        if (rangeTargets.Count == 0)
            return;


        if (consoleButtons.Length != 0)
        {
            foreach (var button in consoleButtons)
            {
                button.Setup(this);
            }
        }


        for (int i = 0; i < rangeTargets.Count; i++)
        {
            rangeTargets[i].Setup(startActive, this);
        }
    }


    public void TargetDied(RangeTarget target)
    {
        rangeTargets.Remove(target);

        target.gameObject.GetComponent<Renderer>().enabled = false;
        target.gameObject.GetComponent<Collider>().enabled = false;
    }
}