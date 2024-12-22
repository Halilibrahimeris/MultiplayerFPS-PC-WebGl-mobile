using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{
    public int CurrentLayer = 0; //0 for Navigation, 1 for Nickname

    [Header("Buttons")]
    public GameObject NavigationParent;
    public GameObject NickNameParent;
    public Button LeftButton;
    public Button RightButton;
    public Button NickNameGo;
    public Button StartButton;
    public Button BackButton;

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
    public GameObject[] Guns = new GameObject[3];


    [Header("Body")]
    public GameObject[] Bodys = new GameObject[3];


    [Header("NickName")]
    public TMP_InputField NickNameField;

    [Header("Settings")]
    public Button SettingsButton;
    public GameObject Cam1;
    public GameObject Cam2;
    public GameObject SettingsCanvas;

    private int index = 0;

    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.instance;
        LeftButton.onClick.AddListener(Left);
        RightButton.onClick.AddListener(Right);
        StartButton.onClick.AddListener(_StartGame);
        NickNameGo.onClick.AddListener(GoNickNameSelection);
        SettingsButton.onClick.AddListener(Settings);
        BackButton.onClick.AddListener(Back);
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
        gameManager.MyNickname = NickNameField.text; 
        SceneManager.LoadScene(1);
    }

    void GoNickNameSelection()
    {
        CurrentLayer = 0;
        BackButton.gameObject.SetActive(true);
        NavigationParent.SetActive(false);
        NickNameParent.SetActive(true);
    }

    void Back()
    {
        GameObject[] objects = { NavigationParent, NickNameParent, SettingsCanvas};
        _Back(objects, CurrentLayer);
        objects.Free();
    }

    void Settings()
    {
        NavigationParent.SetActive(false);
        SettingsCanvas.SetActive(true);
        CurrentLayer = 0;
    }

    private void _Back(GameObject[] parents, int ToBack)
    {
        for (int i = 0; i < parents.Length; i++)
            parents[i].SetActive(false);

        switch (ToBack)
        {
            case 0:
                NavigationParent.SetActive(true);
                Cam1.SetActive(true);
                Cam2.SetActive(false);
                break;
            case 1:
                NickNameParent.SetActive(true);
                Cam1.SetActive(true);
                Cam2.SetActive(false);
                break;
            case 2:
                SettingsCanvas.SetActive(true);
                Cam1.SetActive(false);
                Cam2.SetActive(true);
                break;
            default:
                break;
        }
    }

    void UpdateClasses()
    {
        BackButton.gameObject.SetActive(false);
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


        for (int i = 0; i < Bodys.Length; i++)
        {
            Bodys[i].SetActive(false);
            Guns[i].SetActive(false);
        }
        Bodys[index].SetActive(true);
        Guns[index].SetActive(true);
    }
}
