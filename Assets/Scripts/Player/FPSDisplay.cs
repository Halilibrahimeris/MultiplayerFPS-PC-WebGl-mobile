using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    float deltaTime = 0.0f;
    float timer = 0.0f; // G�ncelleme zamanlay�c�s�
    float updateInterval = 0.7f; // G�ncelleme aral��� (saniye)

    private void Start()
    {
        // V-Sync'i devre d��� b�rak
        QualitySettings.vSyncCount = 0;

        // FPS s�n�r�n� kald�r
        Application.targetFrameRate = 300;
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        // Zamanlay�c�y� g�ncelle
        timer += Time.deltaTime;

        // Belirtilen aral�kta g�ncelleme yap
        if (timer >= updateInterval)
        {
            // G�ncellenmi� FPS ve s�reyi hesapla
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);

            // G�ncellenmi� metni OnGUI metoduna aktar
            fpsText = text;

            // Zamanlay�c�y� s�f�rla
            timer = 0.0f;
        }
    }

    string fpsText = ""; // G�sterilecek FPS metni

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
