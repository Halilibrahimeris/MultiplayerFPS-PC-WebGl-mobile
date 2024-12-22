using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class Settings : MonoBehaviour
{
    [Header("Sensivity")]
    public TMP_InputField SensInputField;



    private void OnDisable()
    {
        float inputcatche = 0f;
        try
        {
            inputcatche = float.Parse(SensInputField.text);
        }
        catch(FormatException)
        {
            Debug.Log("Unable to parse '{inputcatche}");
        }

        GameManager.instance.settingsHolder.sensivity = inputcatche;
    }
}
