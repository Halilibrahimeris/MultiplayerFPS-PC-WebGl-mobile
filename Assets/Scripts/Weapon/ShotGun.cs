using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShotGun : Weapon
{
    [Header("Shotgun")]
    [SerializeField] private int pelletCount = 10; 
    [SerializeField] private float spreadAngle = 10f; 
    [SerializeField] private GameObject bulletTrailPrefab; 
    [SerializeField] private Transform FirePoint;
    [SerializeField] private AnimationClip ReloadStartAnim;
    [SerializeField] private AnimationClip ReloadOneAnim;
    [SerializeField] private bool isFinishedReload = false;
    protected override void Fire()
    {
        ammo--;
        ammoText.text = ammo + "/" + MaxAmmo;

        FireRate = 1 / FireAnim.length;

        Debug.Log("Shotgun Fire");

        for (int i = 0; i < pelletCount; i++)
        {
            // Calculate spread
            Vector3 spread = FirePoint.transform.forward;
            spread += new Vector3(
                Random.Range(-spreadAngle, spreadAngle),
                Random.Range(-spreadAngle, spreadAngle),
                0
            );

            spread.Normalize();

            Ray ray = new Ray(FirePoint.transform.position, spread);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, hitLayer))
            {
                // SpawnBullet trail
                CreateBulletTrail(ray.origin, hit.point);

                if (hit.transform.gameObject.GetComponent<Health>())
                {
                    Debug.Log(hit.collider.gameObject.name);
                    
                    if(hit.transform.GetComponent<BodyIndex>() != null)
                    {
                        int hitIndex = hit.collider.gameObject.GetComponent<BodyIndex>().id;
                        hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, Damage, hitIndex);
                        Health helth = hit.transform.GetComponentInParent<Health>();
                        PlayerInfoText.text = helth.gameObject.name + "/ Health :" + helth.CurrentHealth.ToString();
                    }
                }
            }
        }
    }

    protected override void Reloaded()
    {
        if (MaxAmmo > 0 && ammo < magAmmo)
        {
            _animator.SetBool("Reload", false);
            _animator.SetBool("ShotGunReloadOne", true);

            Debug.Log("ReloadFirst if" + "Ammo:" + ammo);
            MaxAmmo--;
            ammo++;
        }
        else
        {
            isFinishedReload = true;
        }
        ammoText.text = ammo + "/" + MaxAmmo;
        if (!isFinishedReload)
        {
            Invoke("Reloaded", ReloadOneAnim.length);
        }
        else
        {
            _animator.SetBool("ShotGunReloadOne", false);
            _animator.SetBool("Reload", false);
            CanCallAmmo = true;
            isFinishedReload = false;
        }
    }

    protected override void Reload()
    {
        _animator.SetBool("Reload",true);
        Invoke("Reloaded", ReloadStartAnim.length);
        CanCallAmmo = false;
    }

    private void CreateBulletTrail(Vector3 start, Vector3 end)
    {
        GameObject trail = Instantiate(bulletTrailPrefab, start, Quaternion.identity);
        LineRenderer lineRenderer = trail.GetComponent<LineRenderer>();

        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
        }


        Destroy(trail, 0.5f); 
    }
}
