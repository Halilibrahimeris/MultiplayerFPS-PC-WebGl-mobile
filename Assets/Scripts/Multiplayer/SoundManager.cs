using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SoundManager : MonoBehaviour
{
    public AudioSource _AudioSource;
    [Header("Step")]
    public AudioClip[] StepSounds;
    [Header("Fire")]
    public AudioClip[] FireSounds;
    public AudioClip EmptySound;
    [Header("Reload")]
    public AudioClip Reload;


    void SoundPlayer(AudioClip sound, bool needpitch)
    {
        if (needpitch)
        {
            _AudioSource.pitch = Random.Range(0.7f, 1.3f);
            _AudioSource.volume = Random.Range(0.2f, 0.35f);
        }
        _AudioSource.PlayOneShot(sound);
    }
    #region FootStepSounds
    public void PlayFootStep()
    {
        GetComponent<PhotonView>().RPC("StepSound_RPC", RpcTarget.All);
    }

    [PunRPC]
    private void StepSound_RPC()
    {
        int index = Random.Range(0, StepSounds.Length);
        SoundPlayer(StepSounds[index], true);
    }
    #endregion
    #region FireSounds
    public void PlayFireSound()
    {
        GetComponent<PhotonView>().RPC("FireSound_RPC", RpcTarget.All);
    }

    [PunRPC]
    private void FireSound_RPC()
    {
        int index = Random.Range(0, FireSounds.Length);
        SoundPlayer(FireSounds[index], false);
    }

    public void EmptyFireSound()
    {
        GetComponent<PhotonView>().RPC("EmptyFireSound_RPC", RpcTarget.All);
    }

    [PunRPC]
    public void EmptyFireSound_RPC()
    {
        SoundPlayer(EmptySound, false);
    }
    #endregion
    #region ReloadSound

    public void PlayReloadSound()
    {
        GetComponent<PhotonView>().RPC("ReloadSondRPC", RpcTarget.All);
    }
    [PunRPC]
    private void ReloadSondRPC()
    {
        SoundPlayer(Reload, false);
    }

    #endregion
}
