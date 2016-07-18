using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class RangeGameManager : MonoBehaviour
{
    // Using a Property, as to ensure a singleton instance of RangeGameManager
    public static RangeGameManager rgm {get; private set;}


    public GameObject player;

    public bool gameIsOver = false;
    private bool timerActive = false;

    public float timeToComplete = 30f;
    private float timeLeft;

    public int rangesToComplete = 3;

    public Text timeLeftDisplay;
    public GameObject timeLeftBg;

    public GameObject timerActiveGfx;
    public GameObject timerInactiveGfx;

    public GameObject playAgainButton;
    public string playAgainLevelToLoad;

    public GameObject levelSelectUI;
    public bool levelMenuIsShowing = false;

    public GameObject debugUI;
    public bool debugUiIsShowing = false;

    public List<RangeController> ranges;

    private int rangesCompleted = 0;


    // Assign this RangeGameManager as the singleton instance.
    void Awake()
    {
        rgm = this;
    }


    void Start()
    {
        timeLeft = timeToComplete;

        timeLeftDisplay.text = timeLeft.ToString("0.00");

        if (levelSelectUI)
            levelSelectUI.SetActive(false);

        if (debugUI)
            debugUI.SetActive(false);

        if (playAgainButton)
            playAgainButton.SetActive(false);

        if (timerActiveGfx)
            timerActiveGfx.SetActive(false);

        if (timerInactiveGfx)
            timerInactiveGfx.SetActive(true);
    }


    void Update()
    {
        if (levelMenuIsShowing || debugUiIsShowing)
            return;

        if (!gameIsOver)
        {
            if (timeLeft <= 0)
            {
                StopTimer();
                GameOver();
            }
            else if (rangesCompleted >= rangesToComplete)
            {
                StopTimer();
                BeatLevel();
            }
            else if (timerActive)
            {
                timeLeft -= Time.deltaTime;
                timeLeftDisplay.text = timeLeft.ToString("0.00");
            }
        }
    }


    public void RangeCompleted()
    {
        rangesCompleted++;
    }


    public void StopTimer()
    {
        timerInactiveGfx.SetActive(true);
        timerActiveGfx.SetActive(false);
        timerActive = false;
    }


    public void StartTimer()
    {
        timerActiveGfx.SetActive(true);
        timerInactiveGfx.SetActive(false);
        timerActive = true;
    }

    public void showLevelMenu()
    {
        levelMenuIsShowing = true;
        levelSelectUI.SetActive(true);
        MouseLooker.LockCursor(false);
    }

    public void hideLevelMenu()
    {
        levelMenuIsShowing = false;
        levelSelectUI.SetActive(false);
        MouseLooker.LockCursor(true);
    }

    public void showDebugMenu()
    {
        debugUiIsShowing = true;
        debugUI.SetActive(true);
        MouseLooker.LockCursor(false);
    }

    public void hideDebugMenu()
    {
        debugUiIsShowing = false;
        debugUI.SetActive(false);
        MouseLooker.LockCursor(true);
    }

    void GameOver()
    {
        // end the game (failure state)
        timeLeftDisplay.text = "GAME OVER";
        ShowButtons();
        gameIsOver = true;
    }


    void BeatLevel()
    {
        // beat level successfully
        string s = "Finished! With " + timeLeft.ToString("0.00") + " seconds left.";
        timeLeftDisplay.text = s;
        ShowButtons();
        gameIsOver = true;
    }


    void ShowButtons()
    {
        player.GetComponent<CharacterController>().enabled = false;
        timeLeftBg.SetActive(false);
        timerActiveGfx.SetActive(false);
        timerInactiveGfx.SetActive(false);
        playAgainButton.SetActive(true);
        levelSelectUI.SetActive(true);

        MouseLooker.LockCursor(false);
    }
}
// End of Class