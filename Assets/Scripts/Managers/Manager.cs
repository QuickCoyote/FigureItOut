using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Manager : MonoBehaviour
{
    public bool isUIOpen = false;
    public abstract void Resume();
}
