using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;


    public GameObject[] players;

    public Transform[] SpawnPoints;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
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

        ReSpawnPlayer();

    }

    public void ReSpawnPlayer()
    {
        int randomPos = Random.Range(0, SpawnPoints.Length);
        GameObject _player = PhotonNetwork.Instantiate(players[GameManager.instance.Index].name, SpawnPoints[randomPos].position, Quaternion.identity);
        _player.GetComponent<Health>().isLocal = true;
        _player.GetComponent<PlayerSetup>().isLocalPlayer();
        _player.GetComponentInChildren<Health>().SetHealthTexColor(GameManager.instance.classes[GameManager.instance.Index].materialForPlayer.color);
        _player.GetComponentInChildren<Health>().CanSpawn = true;
        //UpdatePlayer(_player);
    }

    /* private void UpdatePlayer(GameObject _player)
     {
         GameManager manager = GameManager.instance;
         _player.GetComponent<MeshRenderer>().material = manager.classes[manager.Index].materialForPlayer;

         _player.GetComponent<PhotonView>().RPC("SetHealth", RpcTarget.All, manager.classes[manager.Index].HP);
         _player.GetComponentInChildren<Health>().SetHealthTexColor(manager.classes[manager.Index].materialForPlayer.color);

         _player.GetComponentInChildren<Weapon>().Damage = manager.classes[manager.Index].Damage;

         _player.GetComponentInChildren<_Movement>().walkSpeed = manager.classes[manager.Index].MoveSpeed;
         _player.GetComponentInChildren<_Movement>().RunSpeed = manager.classes[manager.Index].RunSpeed;
     }*/
}
