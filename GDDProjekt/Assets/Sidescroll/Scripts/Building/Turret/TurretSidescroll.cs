using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TurretSidescroll : BuildingOverClassSidescroll
{
    //public variables
    public GameObject cam;
    public Transform focus;

    //private variables
    Transform oldfocus;
    CinemachineVirtualCamera cinemachineVirtualCamera;
    PlayerMovementSidescroll playerMovementSidescroll;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        playerMovementSidescroll = player.GetComponent<PlayerMovementSidescroll>();
        spriteRenderer = player.GetComponent<SpriteRenderer>();
        cinemachineVirtualCamera = cam.GetComponent<CinemachineVirtualCamera>();
    }

    public override void activateMenu()
    {
        playerMovementSidescroll.inEvent = true;
        spriteRenderer.enabled = false;
        oldfocus = cinemachineVirtualCamera.Follow;
        cinemachineVirtualCamera.Follow = focus;
        transform.GetComponent<TurretWeaponSidescroll>().entered = true;
    }

    public override void deactivateMenu()
    {
        playerMovementSidescroll.inEvent = false;
        spriteRenderer.enabled = true;
        cinemachineVirtualCamera.Follow = oldfocus;
        transform.GetComponent<TurretWeaponSidescroll>().entered = false;
    }
}
