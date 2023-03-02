using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using GooglePlayGames;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;

public class Leaderboard : MonoBehaviour
{
    public void ShowLeaderboardUI()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_leaderboard);
    }
    
    public void DoLeaderboardPost(int _score)
    {
        Social.ReportScore(_score, GPGSIds.leaderboard_leaderboard, (bool success) =>
        {
            if (success)
            {
                Debug.Log("Posted score to leaderboard");
            }
            else
            {
                Debug.Log("Failed to post score");
            }
        });
    }
}
