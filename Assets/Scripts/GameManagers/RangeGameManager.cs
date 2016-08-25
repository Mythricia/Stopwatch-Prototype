using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RangeGameManager : MonoBehaviour
{
    // Using a Property, as to ensure a singleton instance of RangeGameManager
    public static RangeGameManager rgm { get; private set; }
    public string LevelLeaderboardAPIKey;

    public GameObject player;
    public string playerName;
    private bool doNameEntryDialogue = false;
    private bool nameDialogueIsShowing = false;
    public GameObject nameInputField;

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

        if (PlayerPrefs.HasKey("playerName") == false)
        {
            // Do name entry dialogue
            doNameEntryDialogue = true;
        }
        else
        {
            playerName = PlayerPrefs.GetString("playerName");
        }
    }


    void Start()
    {
        nameInputField.GetComponent<InputField>().onEndEdit.AddListener(delegate { finishedNameEntry(nameInputField.GetComponent<InputField>().text); });

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

        if (nameInputField)
            nameInputField.SetActive(false);

        if (doNameEntryDialogue)
            ShowNameEntryDialogue();

    }


    void Update()
    {
        if (levelMenuIsShowing || debugUiIsShowing || nameDialogueIsShowing)
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

        if (Input.GetKeyDown(KeyCode.F10))
        {
            ShowNameEntryDialogue();
        }
    }


    void ShowNameEntryDialogue()
    {
        nameDialogueIsShowing = true;
        nameInputField.SetActive(true);
        player.GetComponent<CharacterController>().enabled = false;
        nameInputField.GetComponent<InputField>().ActivateInputField();
        if (PlayerPrefs.HasKey("playerName"))
            nameInputField.GetComponent<InputField>().text = PlayerPrefs.GetString("playerName");
    }

    void HideNameEntryDialogue()
    {
        nameDialogueIsShowing = false;
        nameInputField.SetActive(false);
        player.GetComponent<CharacterController>().enabled = true;
        nameInputField.GetComponent<InputField>().DeactivateInputField();
    }


    void finishedNameEntry(string enteredName)
    {
        if ((enteredName == null) || enteredName == "")
        {
            ShowNameEntryDialogue();
            return;
        }
        else
        {
            PlayerPrefs.SetString("playerName", enteredName);
            playerName = enteredName;
            Debug.Log("Updated PlayerPrefs to contain playerName: " + playerName);
            HideNameEntryDialogue();
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

        if ((LevelLeaderboardAPIKey != null) && (playerName != null))
        {
            string scoreSubmit = "http://dreamlo.com/lb/" + LevelLeaderboardAPIKey + "/add/" + playerName + "/" + Mathf.RoundToInt(timeLeft * 1000).ToString();
            UnityWebRequest www = UnityWebRequest.Get(scoreSubmit);
            www.Send();
        }

        string s = "Finished! With " + timeLeft.ToString("0.000") + " seconds left.";
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