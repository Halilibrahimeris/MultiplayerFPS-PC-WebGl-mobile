using Photon.Pun;
using Photon.Pun.UtilityScripts;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int CurrentHealth;
    public TextMeshProUGUI HealthText;
    public bool isLocal;
    public bool CanSpawn = true;
    public bool CanTakeDamage = true;
    public void SetHealthTexColor(Color color)
    {
        HealthText.text = CurrentHealth.ToString();
        HealthText.color = color;
    }

    [PunRPC]
    public void TakeDamage(int Damage)
    {
        CurrentHealth -= Damage;
        HealthText.text = CurrentHealth.ToString();
        if(CurrentHealth <= 0)
        {

            if(isLocal && CanSpawn)
            {
                RoomManager.Instance.death++;
                RoomManager.Instance.SetHashes();
                RoomManager.Instance.ReSpawnPlayer();
                CanSpawn = false;
            }

            HealthText.text = "0";
            Destroy(gameObject);
        }
    }
}
