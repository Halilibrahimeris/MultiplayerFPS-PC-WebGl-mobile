using UnityEngine;
using Photon.Pun;
using Hastable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;
using System.Collections.Generic;
public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    public GameObject[] players;

    public Transform[] SpawnPoints;

    public GameObject LoadingCam;

    public int kills = 0;
    public int death = 0;
    public int ping = 0;

    private Dictionary<int, TeamID.MyTeam> playerTeams = new Dictionary<int, TeamID.MyTeam>();
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadingCam.gameObject.SetActive(true);
        Debug.Log("Connecting...");
        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();

        Debug.Log("Connected");
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        PhotonNetwork.JoinOrCreateRoom("Test", null, null);

        Debug.Log("We are in a room");

    }

    public override void OnJoinedRoom()
    {
        Debug.Log("We are in a lobby");
        LoadingCam.gameObject.SetActive(false);
        ReSpawnPlayer();

    }

    public void ReSpawnPlayer()
    {
        int randomPos = Random.Range(0, SpawnPoints.Length);

        GameObject _player = PhotonNetwork.Instantiate(players[GameManager.instance.Index].name, SpawnPoints[randomPos].position, Quaternion.identity);

        _player.GetComponent<Health>().CanTakeDamage = true;
        _player.GetComponent<Health>().isLocal = true;
        _player.GetComponent<Health>().CurrentHealth = GameManager.instance.classes[GameManager.instance.Index].HP;
        _player.GetComponent<PlayerSetup>().isLocalPlayer();
        _player.GetComponentInChildren<Health>().SetHealthTexColor(GameManager.instance.classes[GameManager.instance.Index].materialForPlayer.color);
        _player.GetComponentInChildren<Health>().CanSpawn = true;
        _player.GetComponent<PhotonView>().RPC("SetNickName", RpcTarget.AllBuffered, GameManager.instance.MyNickname);

        int CTCount = TeamCount(TeamID.MyTeam.CT);
        int Tcount = TeamCount(TeamID.MyTeam.T);

        if(CTCount >= Tcount)
            AssignTeam(PhotonNetwork.LocalPlayer, TeamID.MyTeam.T);
        else
            AssignTeam(PhotonNetwork.LocalPlayer, TeamID.MyTeam.CT);

        PhotonNetwork.LocalPlayer.NickName = GameManager.instance.MyNickname;
    }

    private int TeamCount(TeamID.MyTeam team)
    {
        int count = 0;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if(playerTeams.TryGetValue(player.ActorNumber, out TeamID.MyTeam value))
            {
                if(value == team)
                {
                    count += 1;
                }
            }
        }
        return count;
    }

    public void AssignTeam(Player player, TeamID.MyTeam team)
    {
        if (playerTeams.TryGetValue(player.ActorNumber,out TeamID.MyTeam value))
        {
            Debug.Log("Anahtar Bulunmakta");
        }
        else
        {
            Debug.Log("Anahtar Bulunmamakta ve veri eklendi");
            Debug.Log(player.ActorNumber);
            playerTeams[player.ActorNumber] = team;
        }
    }

    public TeamID.MyTeam GetPlayerTeam(Player player)
    {
        if (playerTeams.TryGetValue(player.ActorNumber, out TeamID.MyTeam value))
        {
            return value;
        }
        return TeamID.MyTeam.AllFree; // Varsayýlan olarak AllFree döner
    }

    public void SetHashes()
    {
        try
        {
            Hastable hash = PhotonNetwork.LocalPlayer.CustomProperties;

            hash["kills"] = kills;
            hash["death"] = death;
            hash["ping"] = ping;
            hash["Team"] = TeamID.MyTeam.AllFree;
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
        catch
        {

        }
    }
}
