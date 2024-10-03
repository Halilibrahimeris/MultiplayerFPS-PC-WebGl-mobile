using Photon.Pun;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int CurrentHealth;
    public TextMeshProUGUI HealthText;
    public bool isLocal;
    public bool CanSpawn = true;

    public void SetHealthTexColor(Color color)
    {
        HealthText.text = CurrentHealth.ToString();
        HealthText.color = color;
    }

    [PunRPC]
    public void TakeDamage(int Damage, int hit)
    {
        switch (hit)
        {
            case 0:
                CurrentHealth -= (Damage + 10);
                break;
             case 1:
                CurrentHealth -= Damage;
                break;
            case 2:
                CurrentHealth -= (Damage / 2);
                break;
            default:
                break;
        }
        HealthText.text = CurrentHealth.ToString();
        if(CurrentHealth <= 0)
        {
            Destroy(gameObject);
            if(isLocal && CanSpawn)
            {
                RoomManager.Instance.ReSpawnPlayer();
                CanSpawn = false;
            }
            HealthText.text = "0";
        }
    }
}
