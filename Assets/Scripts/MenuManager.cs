using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] List<Canvas> canvases = new List<Canvas>();
    public Image soundIcon;
    BallSpriteManager ballSpriteManager;
    AudioSource m_MyAudioSource;
    AudioManager audioManager;
    int click = 0;


    // Start is called before the first frame update
    void Start()
    {
        canvases[0].gameObject.SetActive(true);
        canvases[1].gameObject.SetActive(false);
        canvases[2].gameObject.SetActive(false);
        canvases[3].gameObject.SetActive(false);
        ballSpriteManager = GameObject.Find("BallSpriteManager").GetComponent<BallSpriteManager>();
        audioManager = GameObject.Find("BallSpriteManager").GetComponent<AudioManager>();
        m_MyAudioSource = ballSpriteManager.GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("Music"))
        {
            audioManager.Sound = PlayerPrefs.GetInt("Music") == 1 ? true : false;
            if (audioManager.Sound == true)
            {
                m_MyAudioSource.Play();
            }
            else
            {
                m_MyAudioSource.Stop();
            }
        }
        else
        {
            audioManager.Sound = true;
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (audioManager.Sound == true)
        {
            soundIcon.gameObject.SetActive(true);
        }
        else
        {
            soundIcon.gameObject.SetActive(false);
        }

        if (canvases[1].gameObject.activeSelf==true|| canvases[2].gameObject.activeSelf == true|| canvases[3].gameObject.activeSelf == true)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                // Check if Back was pressed this frame
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    canvases[0].gameObject.SetActive(true);
                    canvases[1].gameObject.SetActive(false);
                    canvases[2].gameObject.SetActive(false);
                    canvases[3].gameObject.SetActive(false);
                }
            }
            
        }
        if (canvases[0].gameObject.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                click++;
                StartCoroutine(ClickTime());

                if (click > 1)
                {
                    Application.Quit();
                }
            }
        }


    }
    IEnumerator ClickTime()
    {
        yield return new WaitForSeconds(0.5f);
        click = 0;
    }
    public void MenuBtns(string str)
    {
        if (str == "Settings")
        {
            canvases[0].gameObject.SetActive(false);
            canvases[1].gameObject.SetActive(true);
        }
        else if (str == "Customize")
        {
            canvases[0].gameObject.SetActive(false);
            canvases[2].gameObject.SetActive(true);
        }
        else if (str == "Tutorial")
        {
            canvases[0].gameObject.SetActive(false);
            canvases[3].gameObject.SetActive(true);
        }
        else if (str == "Back")
        {
            canvases[0].gameObject.SetActive(true);
            canvases[1].gameObject.SetActive(false);
            canvases[2].gameObject.SetActive(false);
            canvases[3].gameObject.SetActive(false);
        }
    }
    public void ChangeBall(string str)
    {
        ballSpriteManager.ChangeBall(str);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    public void Sound()
    {
        if (audioManager.Sound == true)
        {
            AudioListener.pause = true;
            audioManager.Sound = false;
            soundIcon.gameObject.SetActive(false);
            m_MyAudioSource.Stop();
            PlayerPrefs.SetInt("Music", audioManager.Sound ? 1 : 0);
            PlayerPrefs.Save();

        }
        else
        {
            AudioListener.pause = false;
            audioManager.Sound = true;
            soundIcon.gameObject.SetActive(true);
            m_MyAudioSource.Play();
            PlayerPrefs.SetInt("Music", audioManager.Sound ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    
}
