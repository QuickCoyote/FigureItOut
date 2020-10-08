using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GlobalManager : Singleton<GlobalManager>
{
    public bool playerInControl = true;

    public PlayerAimController playerAimController = null;
    public PlayerMovementController playerMovementController = null;

    public OptionsManager optionsManager = null;

    public Dictionary<int, GameObject> entityDictionary = new Dictionary<int, GameObject>();

    void Update()
    {
        CheckUIOpen();
    }

    public void CheckUIOpen()
    {
        playerInControl = !optionsManager.isUIOpen; // This is saying that isUIOpen == true, then playerInControl == false; and vice versa
        if (!playerInControl) return; // This is done so that we don't set the playerInControl if the player already has lost control and we kick out so no more logic is done.
    }

    public void UpdatePlayerSensitivity()
    {
        playerAimController.horizontalSensitivity = PlayerPrefs.GetFloat("HorizontalSensitivity");
        playerAimController.verticalSensitivity = PlayerPrefs.GetFloat("VerticalSensitivity");
    }
}
