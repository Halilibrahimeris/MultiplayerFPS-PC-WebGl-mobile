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
    public LayerMask hitLayer;
    [Header("UI")]
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI PlayerInfoText;

    [Header("Animation")]
    public Animator _animator;
    public AnimationClip ReloadAnim;
    public AnimationClip FireAnim;
    public int FireAnimationMultiplayer = 1;
    private void Start()
    {
        ammoText.text = ammo + "/" + MaxAmmo;
        FireRate = (1 / FireAnim.length) * FireAnimationMultiplayer;
        PlayerInfoText.text = "";
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

            if (Input.GetKeyDown(KeyCode.R) && CanCallAmmo && MaxAmmo > 0 && ammo < magAmmo)
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

        if(Physics.Raycast(ray.origin,ray.direction,out hit, 100f, hitLayer))
        {
            if (hit.transform.gameObject.GetComponent<Health>())
            {
                Debug.Log(hit.collider.gameObject.name);
                int hitIndex = hit.collider.gameObject.GetComponent<BodyIndex>().id;
                hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, Damage,hitIndex);
                Health helth = hit.transform.GetComponentInParent<Health>();
                PlayerInfoText.text = helth.gameObject.name + "/ Health :" + helth.CurrentHealth.ToString();
            }
        }

    }
}
