using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] GameObject canvas,gameElements;
    public int score;
    private float timer;
    [SerializeField] private Text scoreText;
    public TextMeshProUGUI finalScoreText;
    BallManager ball;
    BarrierManager barrier;
    bool StartGame;
    public Canvas pauseMenu;
    public Button pausebtn;
    Leaderboard leaderboard;

    public void EndGame()
    {
        leaderboard.DoLeaderboardPost(score);
        StartGame = false;
        ball.gameObject.SetActive(false);
        finalScoreText.text = score.ToString();
        gameElements.SetActive(false);
        canvas.SetActive(true);


    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.gameObject.SetActive(true);
        pausebtn.gameObject.SetActive(false);
    }
    public void Resume()
    {
        pauseMenu.gameObject.SetActive(false);
        pausebtn.gameObject.SetActive(true);
        Time.timeScale = 1;
    }

    void Start()
    {
        leaderboard = GameObject.Find("BallSpriteManager").GetComponent<Leaderboard>();
        Time.timeScale = 1;
        ball = GameObject.Find("Ball").GetComponent<BallManager>();
        barrier = GameObject.Find("Ball").GetComponent<BarrierManager>();
        ball.playerSpeed = 150f;
        score = 1;
        timer = 0;
        StartGame = true;
    }
    void Update()
    {
        //if (ball.transform.position.z >= barrier.barriers[0].transform.position.z&& barrier.barriers[0].name == "Plasma(Clone)")
        //{
        //    if (score > 500)
        //    {
        //        RenderSettings.skybox = materials[2];
        //    }
        //    else if (score > 250)
        //    {
        //        RenderSettings.skybox = materials[1];
        //    }
        //    else
        //    {
        //        RenderSettings.skybox = materials[0];
        //    }
        //}
        if (ball.transform.position.z > barrier.barriers[0].transform.position.z)
        {
            Destroy(barrier.barriers[0]);
            barrier.barriers.RemoveAt(0);
            
        }



        timer += Time.deltaTime;

        if (timer > 0.1f&& StartGame==true)
        {

            score += 1;

            //We only need to update the text if the score changed.
            scoreText.text = score.ToString();

            //Reset the timer to 0.
            timer = 0;
        }

        if (score % 100 == 0&& StartGame == true)
        {
            ball.playerSpeed += 5f;
            score++;
        }
    }
    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
