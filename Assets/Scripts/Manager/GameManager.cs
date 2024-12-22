using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Classes
{
    public string Name;
    public int ID;
    public Material materialForPlayer;
    public int HP;
    public int Damage;
    public int MoveSpeed;
    public int RunSpeed;
    public int BulletPerSecond;
}

[Serializable]
public class SettingsHolder
{
    public float sensivity;
}

public class GameManager : MonoBehaviour
{
    public List<Classes> classes;
    public SettingsHolder settingsHolder;
    public static GameManager instance;
    [Space]
    public string MyNickname;
    [Space]
    public List<Material> materialsForPlayer;
    public int Index;
    [Space]
    public string myIp;

    void Awake()
    {
        // Singleton pattern to ensure only one instance of GameManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
