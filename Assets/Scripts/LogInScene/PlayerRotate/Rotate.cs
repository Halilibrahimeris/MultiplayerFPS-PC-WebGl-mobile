using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float Speed;

    private void Update()
    {
        transform.Rotate(0, Speed * Time.deltaTime, 0);
    }
}
