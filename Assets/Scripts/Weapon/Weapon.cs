using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponAnimationEvent weaponAnimationEvent;
    [Space]
    public int Damage;
    [SerializeField] protected float FireRate;
    [Space]
    private float NextFire;
    public Camera _camera;
    [Header("Ammo")]
    public int MaxAmmo = 150;
    public int ammo = 30;
    public int magAmmo = 30;
    protected bool CanCallAmmo = true;
    [Header("UI")]
    public TextMeshProUGUI ammoText;

    [Header("Animation")]
    public Animator _animator;
    public AnimationClip ReloadAnim;
    public AnimationClip FireAnim;
    private void Start()
    {
        ammoText.text = ammo + "/" + MaxAmmo;
        FireRate = 1 / FireAnim.length;
    }

    // Update is called once per frame
    void Update()
    {

        if (weaponAnimationEvent.CanTake)
        {
            if (NextFire > 0)
                NextFire -= Time.deltaTime;


            if (Input.GetButton("Fire1") && NextFire <= 0 && ammo > 0 && CanCallAmmo)
            {
                NextFire = 1 / FireRate;
                //recoil.CanRecoil = true;
                Fire();
                _animator.SetBool("Inspecting", false);
            }

            else if(Input.GetButton("Fire1") && NextFire <= 0 && ammo <= 0 && CanCallAmmo)
            {
                Debug.Log("Mermi yok sesi çalacak");
            }

            if(ammo == 0 && CanCallAmmo && MaxAmmo > 0)
            {
                Reload();
            }

            if (Input.GetKeyDown(KeyCode.R) && CanCallAmmo && MaxAmmo > 0)
            {
                Reload();
            }

            if (Input.GetKey(KeyCode.Y))
            {
                _animator.SetBool("Inspecting", true);
            }

            _animator.SetBool("Shoot", Input.GetButton("Fire1"));
        }

    }

    protected virtual void Reload()
    {
        Invoke("Reloaded", ReloadAnim.length);
        _animator.SetBool("InReload", true);
        _animator.SetTrigger("Reload");
        CanCallAmmo = false;
    }

    protected virtual void Reloaded()
    {
        if (MaxAmmo > 0)
        {
            if (MaxAmmo > magAmmo - ammo)
            {
                MaxAmmo -= (magAmmo - ammo);
                ammo = magAmmo;
            }
            else
            {
                ammo += MaxAmmo;
                MaxAmmo = 0;
            }
        }
        ammoText.text = ammo + "/" + MaxAmmo;
        CanCallAmmo = true;
        _animator.SetBool("InReload", false);
    }

    protected virtual void Fire()
    {
        ammo--;
        ammoText.text = ammo + "/" + MaxAmmo;

        Debug.Log("Fire");
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);

        RaycastHit hit;

        if(Physics.Raycast(ray.origin,ray.direction,out hit, 100f))
        {
            if (hit.transform.gameObject.GetComponent<Health>())
            {
                Debug.Log(hit.collider.gameObject.name);
                hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, Damage, hit.collider);
            }
        }

    }
}
