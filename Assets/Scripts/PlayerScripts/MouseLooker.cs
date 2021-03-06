﻿using UnityEngine;

public class MouseLooker : MonoBehaviour
{

    // Use this for initialization
    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public bool clampVerticalRotation = true;
    public float MinimumX = -90F;
    public float MaximumX = 90F;

    // internal private variables
    private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;
    private Transform character;
    private Transform cameraTransform;

    public static bool doRotation = true;

    public bool lockMouseByDefault = false;


    void Start()
    {
        // start the game with the cursor locked or unlocked according to option
        LockCursor(lockMouseByDefault);

        // get a reference to the player transform
        character = gameObject.transform;

        // get a reference to the main camera's transform
        cameraTransform = Camera.main.transform;

        // get the location rotation of the character and the camera
        m_CharacterTargetRot = character.localRotation;
        m_CameraTargetRot = cameraTransform.localRotation;

    }


    void Update()
    {
        // rotate stuff based on the mouse
        if (doRotation)
        {
            LookRotation();
        }

        // if ESCAPE key is pressed, then unlock the cursor
        if (Input.GetButtonDown("Cancel"))
        {
            LockCursor(false);
        }

        // if the player fires, then relock the cursor, unless a menu is open
        if (Input.GetButtonDown("Fire1") && !(RangeGameManager.rgm.gameIsOver || RangeGameManager.rgm.levelMenuIsShowing || RangeGameManager.rgm.debugUiIsShowing))
        {
            LockCursor(true);
        }

        if (Input.GetButtonDown("LevelSelect"))
        {
            if (RangeGameManager.rgm.levelMenuIsShowing)
                RangeGameManager.rgm.hideLevelMenu();
            else if (RangeGameManager.rgm.levelMenuIsShowing == false)
                RangeGameManager.rgm.showLevelMenu();
        }

        if (Input.GetButtonDown("DebugMenu"))
        {
            if (RangeGameManager.rgm.debugUiIsShowing)
                RangeGameManager.rgm.hideDebugMenu();
            else if (RangeGameManager.rgm.debugUiIsShowing == false)
                RangeGameManager.rgm.showDebugMenu();
        }
    }


    public static void LockCursor(bool isLocked)
    {
        if (isLocked)
        {
            doRotation = true;

            // make the mouse pointer invisible
            Cursor.visible = false;

            // lock the mouse pointer within the game area
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            doRotation = false;
            // make the mouse pointer visible
            Cursor.visible = true;

            // unlock the mouse pointer so player can click on other windows
            Cursor.lockState = CursorLockMode.None;
        }
    }


    public void LookRotation()
    {
        //get the y and x rotation based on the Input manager
        float xRot = Input.GetAxisRaw("Mouse Y") * YSensitivity;
        float yRot = Input.GetAxisRaw("Mouse X") * XSensitivity;

        // calculate the rotation
        m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
        m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

        // clamp the vertical rotation if specified
        if (clampVerticalRotation)
            m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);

        character.localRotation = m_CharacterTargetRot;
        cameraTransform.localRotation = m_CameraTargetRot;
    }

    // Some math ... that I don't understand...
    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}
