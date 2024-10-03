using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float SwayClamp = 0.05f;

    public float Smoothing = 1.5f;

    private Vector3 orgPos;

    private void Start()
    {
        orgPos = transform.localPosition;
    }

    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        input.x = Mathf.Clamp(input.x, -SwayClamp, SwayClamp);
        input.y = Mathf.Clamp(input.y, -SwayClamp, SwayClamp);

        Vector3 target = new Vector3(-input.x, -input.y, 0f);

        transform.localPosition = Vector3.Lerp(transform.localPosition, target + orgPos, Time.deltaTime * Smoothing);
    }
}
