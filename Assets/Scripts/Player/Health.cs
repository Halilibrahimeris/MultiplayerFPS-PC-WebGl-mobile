using Photon.Pun;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int CurrentHealth;
    public TextMeshProUGUI HealthText;
    public bool isLocal;
    public bool CanSpawn = true;

    [SerializeField]private Collider[] colliders = new Collider[3];
    public void SetHealthTexColor(Color color)
    {
        HealthText.text = CurrentHealth.ToString();
        HealthText.color = color;
    }

    [PunRPC]
    public void TakeDamage(int Damage, Collider hit)
    {
        if (hit == colliders[0])//head
        {
            CurrentHealth -= (Damage + 10);
        }
        else if (hit == colliders[1])//body
        {
            CurrentHealth -= Damage;
        }
        else//leg
        {
            CurrentHealth -= (Damage / 2);
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
