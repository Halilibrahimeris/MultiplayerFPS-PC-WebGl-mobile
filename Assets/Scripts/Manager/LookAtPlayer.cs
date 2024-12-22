using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LookAtPlayer : MonoBehaviour
{
    void Update()
    {
        if(!Camera.main.transform.GetComponent<PlayerSetup>().isLocal)
            transform.LookAt(Camera.main.transform);
    }
}
