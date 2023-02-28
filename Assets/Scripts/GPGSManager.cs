using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class GPGSManager : MonoBehaviour
{
    private PlayGamesClientConfiguration clientConfiguration;
    public Text statusTxt;
    public Text descriptionTxt;

    private void Start()
    {
        ConfigureGPGS();
        SignIntoGPGS(SignInInteractivity.CanPromptOnce, clientConfiguration);
    }
    public void ConfigureGPGS()
    {
        clientConfiguration = new PlayGamesClientConfiguration.Builder().Build();
    }
         void SignIntoGPGS(SignInInteractivity interactivity, PlayGamesClientConfiguration configuration)
    {
        configuration = clientConfiguration;
        PlayGamesPlatform.InitializeInstance(configuration);
        PlayGamesPlatform.Activate();

        PlayGamesPlatform.Instance.Authenticate(interactivity, (code) =>
        {
            statusTxt.text = "Authenticating...";
            if (code == SignInStatus.Success)
            {
                statusTxt.text = "Successfully";
                descriptionTxt.text = Social.localUser.userName + "\n" + Social.localUser.id;
            }
            else
            {
                statusTxt.text = "Failed";
                descriptionTxt.text ="Failed to Auth, reason for failure is " + code;
            }
        });
            
    }

    public void BasicSignInBtn()
    {
        SignIntoGPGS(SignInInteractivity.CanPromptAlways, clientConfiguration);
    }

    public void SignOutBtn()
    {
        PlayGamesPlatform.Instance.SignOut();
        statusTxt.text = "Signed Out";
        
    }
}
