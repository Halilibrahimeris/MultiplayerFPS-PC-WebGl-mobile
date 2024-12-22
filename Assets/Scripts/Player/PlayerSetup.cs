using Photon.Pun;
using TMPro;
using UnityEngine;
public class PlayerSetup : MonoBehaviour
{
    public _Movement movement;
    public FPSDisplay FPSDisplay;
    public GameObject Camera;
    public GameObject HandsForOutside;

    public TextMeshProUGUI nickField;
    public string myNick;
    public bool isLocal;
    public void isLocalPlayer()
    {
        movement.enabled = true;
        FPSDisplay.enabled = true;
        Camera.SetActive(true);
        HandsForOutside.SetActive(false);
        isLocal = true;
    }

    [PunRPC]
    public void SetNickName(string nickName)
    {
        nickField.text = nickName;
        myNick = nickName;
    }
}
