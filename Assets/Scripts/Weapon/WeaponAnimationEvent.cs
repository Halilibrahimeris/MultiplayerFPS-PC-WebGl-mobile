using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationEvent : MonoBehaviour
{
    public bool CanTake;
    public Animator animator;
    public void CanTakeAnyThing()
    {
        animator.SetBool("CanTake", true);
        CanTake = true;
    }

    public void CantTakeAnyThing()
    {
        animator.SetBool("CanTake", false);
        CanTake = false;
    }
}
