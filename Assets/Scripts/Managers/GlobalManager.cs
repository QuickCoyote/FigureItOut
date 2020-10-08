using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : Singleton<GlobalManager>
{
    public bool playerInControl = true;

    public GameObject player = null;

    public OptionsManager optionsManager = null;

    void Start()
    {
        
    }

    void Update()
    {
        CheckUIOpen();
    }

    public void CheckUIOpen()
    {
        if(playerInControl)
        {
            playerInControl = !optionsManager.isUIOpen;
            if (!playerInControl) return; // This is done so that we don't set the playerInControl if the player already has lost control and we kick out so no more logic is done.
        }
    }
}
