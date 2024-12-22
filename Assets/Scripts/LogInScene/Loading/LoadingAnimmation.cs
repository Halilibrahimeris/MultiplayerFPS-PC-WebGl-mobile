using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingAnimmation : MonoBehaviour
{
    public TextMeshProUGUI loadingText;

    private void Start()
    {
        StartCoroutine(ChangeLoadingText());
    }

    private IEnumerator ChangeLoadingText()
    {
        string[] loadingStrings = { "Loading", "Loading.", "Loading..", "Loading..." };
        int index = 0;

        while (true)
        {
            loadingText.text = loadingStrings[index];
            index = (index + 1) % loadingStrings.Length;

            yield return new WaitForSeconds(0.5f);
        }
    }
}
