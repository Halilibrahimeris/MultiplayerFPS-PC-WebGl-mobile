using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public _Movement movement;
    public FPSDisplay FPSDisplay;
    public GameObject Camera;
    public GameObject HandsForOutside;

    public void isLocalPlayer()
    {
        movement.enabled = true;
        FPSDisplay.enabled = true;
        Camera.SetActive(true);
        HandsForOutside.SetActive(false);
    }
}
