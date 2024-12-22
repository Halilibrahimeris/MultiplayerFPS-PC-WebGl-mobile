
using UnityEngine;
using System.Linq;
using Photon.Pun;
using TMPro;
using Photon.Pun.UtilityScripts;

public class LeaderBoardManager : MonoBehaviour
{
    public GameObject playerHolder;
    public GameObject BG;

    [Header("Options")]
    public float refreshRate;

    [Header("UI")]
    public GameObject[] Slots;

    [Space]
    public TextMeshProUGUI[] NameTexts;
    public TextMeshProUGUI[] ScoreTexts;
    public TextMeshProUGUI[] KillTexts;
    public TextMeshProUGUI[] PingTexts;


    private void Start()
    {
        NameTexts = new TextMeshProUGUI[Slots.Length];
        ScoreTexts = new TextMeshProUGUI[Slots.Length];
        KillTexts = new TextMeshProUGUI[Slots.Length];
        PingTexts = new TextMeshProUGUI[Slots.Length];

        for (int i = 0; i < Slots.Length; i++)
        {
            Debug.Log("kill,score cart curt alýndý");
            var texts = Slots[i].GetComponentsInChildren<TextMeshProUGUI>();//Main texts
            Debug.Log(texts.Length);

            NameTexts[i] = texts[0];
            ScoreTexts[i] = texts[1];
            KillTexts[i] = texts[2];
            PingTexts[i] = texts[3];

        }
        InvokeRepeating(nameof(Refresh), 1f, refreshRate);
    }


    public void Refresh()
    {
        foreach (var slot in Slots) 
        {
            slot.SetActive(false);
        }

        var sortedPlayerList = (from player in PhotonNetwork.PlayerList orderby player.GetScore() descending select player).ToList();

        int i = 0;
        foreach (var player in sortedPlayerList) 
        {
            Slots[i].SetActive(true);

            if (player.NickName == "")
                player.NickName = "UnKnown";

            NameTexts[i].text = player.NickName;
            if (player.CustomProperties["kills"] != null)
            {
                ScoreTexts[i].text = player.CustomProperties["kills"] + "";
                KillTexts[i].text = player.CustomProperties["death"] + "";
                PingTexts[i].text = player.CustomProperties["ping"] + "";
            }
            else
            {
                ScoreTexts[i].text = "0";
                KillTexts[i].text = "0";
                PingTexts[i].text = "N/a";
            }

            i++;
        }
    }

    private void Update()
    {
        BG.SetActive(Input.GetKey(KeyCode.Tab));
    }

}
