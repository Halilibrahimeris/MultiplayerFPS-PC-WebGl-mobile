using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationEvent : MonoBehaviour
{
    public bool CanTake;
    public Animator animator;

    [Header("Sounds")]
    public SoundManager soundManager;
    [HideInInspector] public bool EmptySound = false;
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

    public void PlayStepSounds()
    {
        soundManager.PlayFootStep();
    }

    public void PlayShootSound()
    {
        if (!EmptySound)
            soundManager.PlayFireSound();
        else
            soundManager.EmptyFireSound();

    }
    
    public void PlayReloadSound()
    {
        soundManager.PlayReloadSound();
    }
}
