using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicManager : MonoBehaviour
{
    public static PublicManager instance;

    private void Awake()
    {
        instance = this;
    }
}
