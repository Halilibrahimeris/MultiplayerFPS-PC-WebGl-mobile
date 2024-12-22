using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class FPSDisplay : MonoBehaviour
{
    float deltaTime = 0.0f;
    float timer = 0.0f; // Güncelleme zamanlayýcýsý
    float updateInterval = 1f; // Güncelleme aralýðý (saniye)
    int ping;
    private void Start()
    {
        // V-Sync'i devre dýþý býrak
        QualitySettings.vSyncCount = 0;

        // FPS sýnýrýný kaldýr
        Application.targetFrameRate = 300;
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        // Zamanlayýcýyý güncelle
        timer += Time.deltaTime;

        // Belirtilen aralýkta güncelleme yap
        if (timer >= updateInterval)
        {
            // Güncellenmiþ FPS ve süreyi hesapla
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            ping = PhotonNetwork.GetPing();

            string text = string.Format("{0:0.0} ms ({1:0.} fps) ({2} ping)", msec, fps, ping);

            RoomManager.Instance.ping = ping;
            RoomManager.Instance.SetHashes();

            // Güncellenmiþ metni OnGUI metoduna aktar
            fpsText = text;

            // Zamanlayýcýyý sýfýrla
            timer = 0.0f;
        }
    }

    string fpsText = ""; // Gösterilecek FPS metni

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperRight;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = Color.white;

        GUI.Label(rect, fpsText, style);
    }
}
