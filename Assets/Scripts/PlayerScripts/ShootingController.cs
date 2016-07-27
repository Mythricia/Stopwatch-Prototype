﻿using UnityEngine;
using System.Collections.Generic;

public class ShootingController : MonoBehaviour
{
    private List<Weapon> myWeapons = new List<Weapon>();
    public int weaponIndex = 0;


    // interaction stuff
    private Transform playerCameraTransform;
    private RaycastHit hit;
    public float interactRange = 3.0f;
    private int layerMask = 1 << 8;     // Only interact with objects that are part of the custom "Interactable" Layer.
                                        // (0000 0001) << 8 = (1 0000 0000)  -> Only interact with the 8th layer

    public void Start()
    {
        playerCameraTransform = Camera.main.transform;

        myWeapons.AddRange(gameObject.GetComponents<Weapon>());

        myWeapons.Reverse();

        foreach (var weapon in myWeapons)
        {
            weapon.Initialize();
        }
    }


    public void Update()
    {
        if (Input.GetButtonDown("Fire1") && MouseLooker.doRotation && myWeapons.Count != 0)
        {
            myWeapons[weaponIndex].FireWeapon();
        }


        if (Input.GetButtonDown("Interact"))
        {
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.TransformDirection(Vector3.forward), out hit, interactRange, layerMask))
            {
                if (hit.collider.gameObject.GetComponent<BasicRangeTrigger>() != null)
                {
                    hit.collider.gameObject.GetComponent<BasicRangeTrigger>().Trigger();
                }
            }
        }
    } // End of Update()
}
