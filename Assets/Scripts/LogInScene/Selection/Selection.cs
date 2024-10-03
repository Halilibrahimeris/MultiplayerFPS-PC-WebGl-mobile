using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{
    [Header("Buttons")]
    public Button LeftButton;
    public Button RightButton;
    public Button StartButton;

    [Header("Sliders")]
    public Slider HPSider;
    public Slider DamageSlider;
    public Slider MoveSpeedSlider;
    public Slider RunSpeedSlider;
    public Slider BulletPerSecondSlider;

    [Header("Max_Values")]
    public TextMeshProUGUI HPSliderValue;
    public TextMeshProUGUI DamageSliderValue;
    public TextMeshProUGUI MoveSpeedSliderValue;
    public TextMeshProUGUI RunSpeedSliderValue;
    public TextMeshProUGUI BulletPerSecondSliderValue;

    [Header("Guns")]
    public GameObject Scar;
    public GameObject Shotgun;
    public GameObject CZ_Scorpion;

    private int index = 0;
    [Space]
    public GameObject Player;
    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.instance;
        LeftButton.onClick.AddListener(Left);
        RightButton.onClick.AddListener(Right);
        StartButton.onClick.AddListener(_StartGame);
        UpdateClasses();
    }

    void Left()
    {
        index--;
        if(index == -1)
            index = gameManager.classes.Count - 1;

        UpdateClasses();
    }
    void Right()
    {
        index++;
        if (index == gameManager.classes.Count)
            index = 0;

        UpdateClasses();
    }
    void _StartGame()
    {
        gameManager.Index = index;
        SceneManager.LoadScene(1);
    }
    void UpdateClasses()
    {
        Player.GetComponent<MeshRenderer>().material = gameManager.materialsForPlayer[index];

        HPSider.value = gameManager.classes[index].HP;
        HPSliderValue.text = HPSider.value.ToString();

        DamageSlider.value = gameManager.classes[index].Damage;
        DamageSliderValue.text = DamageSlider.value.ToString();

        MoveSpeedSlider.value = gameManager.classes[index].MoveSpeed;
        MoveSpeedSliderValue.text = MoveSpeedSlider.value.ToString();

        RunSpeedSlider.value = gameManager.classes[index].RunSpeed;
        RunSpeedSliderValue.text = RunSpeedSlider.value.ToString();

        BulletPerSecondSlider.value = gameManager.classes[index].BulletPerSecond;
        BulletPerSecondSliderValue.text = BulletPerSecondSlider.value.ToString();

        switch (index)
        {
            case 0:
                Scar.SetActive(true);
                CZ_Scorpion.SetActive(false);
                break;
            case 1:
                Scar.SetActive(false);
                Shotgun.SetActive(true);
                break;
            case 2:
                Shotgun.SetActive(false);
                CZ_Scorpion.SetActive(true);
                break;
            default:
                break;
        }
    }
}
