using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMP_Text WelcomeText;

    private void Start()
    {
        if(DatabaseManager.PlayerId == 0)
        {
            WelcomeText.text = "Lobby \n Please Login or Register or Play Offline";
        }
        else if(DatabaseManager.PlayerId >= 1) 
        {
            WelcomeText.text = "Welcome " + DatabaseManager.PlayerName;
        }
    }

    public void GoToRegister()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToLogin()
    {
        SceneManager.LoadScene(2);
    }
}
