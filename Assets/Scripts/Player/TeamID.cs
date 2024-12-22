using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamID : MonoBehaviour
{
    public enum MyTeam
    {
        CT,
        T,
        AllFree
    }

    public MyTeam m_Team;

    private void Start()
    {
        // Eðer bu karakter local player'a aitse, takým bilgisi TeamManager'dan alýnacak
        if (GetComponent<PhotonView>().IsMine)
        {
            m_Team = RoomManager.Instance.GetPlayerTeam(PhotonNetwork.LocalPlayer);
        }
    }
}
