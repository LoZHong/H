using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class Login : MonoBehaviour
{
    [Header("InputField")]
    public TMP_InputField Player_Name;
    public TMP_InputField Password;
    public Button SubmitButton;

    public void CallLogin()
    {
        StartCoroutine(login());
    }

    IEnumerator login()
    {
        WWWForm form = new WWWForm();
        form.AddField("PlayerName", Player_Name.text);
        form.AddField("Password", Password.text);

        // Use UnityWebRequest.Post with a URI and a WWWForm
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/sql_connector/login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log(www.downloadHandler.text + www.result.ToString());
                string jsonResponse = www.downloadHandler.text;
                try
                {
                    Debug.Log("Received JSON Response: " + jsonResponse);

                    // Parse the JSON response into a LoginResponse object
                    LoginResponse response = JsonUtility.FromJson<LoginResponse>(jsonResponse);

                    // Extract data from the response and assign to variables
                    DatabaseManager.PlayerId = response.PlayerId;
                    DatabaseManager.PlayerName = response.PlayerName;
                    DatabaseManager.TimeJson = JsonUtility.FromJson<CurrentTime>(response.TimeJson);

                    SceneManager.LoadScene(0);
                }
                catch (Exception e)
                {
                    // Handle any JSON parsing errors
                    Debug.LogError("JSON Parsing Error: " + e.Message);
                }
            }
            else
            {
                Debug.Log("User Creation Failed: " + www.error);
            }
        }
    }

    public void VerifuInputs()
    {
        // Enable the submit button if input fields meet the required length
        SubmitButton.interactable = (Player_Name.text.Length >= 3 && Password.text.Length >= 6);
    }

    public void BackToMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene(0);
    }
}

// Serializable class to hold the JSON response data
[System.Serializable]
public class LoginResponse
{
    public string status;
    public string message;
    public int PlayerId;
    public string PlayerName;
    public string TimeJson;
}