using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BallSpriteManager : MonoBehaviour
{
    [SerializeField] List<Material> materials = new List<Material>();
    public Material ballMat;
    public static BallSpriteManager singleton = null;

    public static BallSpriteManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

    }
    void Start()
    {
        string str;
        ballMat = materials[0];
        if (PlayerPrefs.HasKey("Ball"))
        {
            str = PlayerPrefs.GetString("Ball");
            ChangeBall(str);
        }
        else
        {
            str = "1";
            ChangeBall(str);
        }
    }
    private void Update()
    {
        
    }
    public void ChangeBall(string str)
    {
        if (str == "1")
        {
            ballMat = materials[0];
            Debug.Log("fireball");
            PlayerPrefs.SetString("Ball", "1");
            PlayerPrefs.Save();
        }
        else if (str == "2")
        {
            ballMat = materials[1];
            Debug.Log("golden");
            PlayerPrefs.SetString("Ball", "2");
            PlayerPrefs.Save();
        }
        else if (str == "3")
        {
            ballMat = materials[2];
            Debug.Log("purple");
            PlayerPrefs.SetString("Ball", "3");
            PlayerPrefs.Save();
        }
        else if (str == "4")
        {
            ballMat = materials[3];
            Debug.Log("sand");
            PlayerPrefs.SetString("Ball", "4");
            PlayerPrefs.Save();
        }
        else if (str == "5")
        {
            ballMat = materials[4];
            Debug.Log("shield");
            PlayerPrefs.SetString("Ball", "5");
            PlayerPrefs.Save();
        }
        else if (str == "6")
        {
            ballMat = materials[5];
            Debug.Log("water");
            PlayerPrefs.SetString("Ball", "6");
            PlayerPrefs.Save();
        }

    }
    
}
