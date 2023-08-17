using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class Registeration : MonoBehaviour
{
    [Header("InputField")]
    public TMP_InputField Player_Name;
    public TMP_InputField Email;
    public TMP_InputField Password;
    public TMP_InputField ComfirmPassword;
    public Button SubmitButton;

    public TextAsset TimeJson;
    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("PlayerName", Player_Name.text);
        form.AddField("Email", Email.text);
        form.AddField("Password", Password.text);
        form.AddField("TimeJson", TimeJson.text);

        // Use UnityWebRequest.Post with a URI and a WWWForm
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/sql_connector/register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("User Created Successfully." + www.downloadHandler.text);
                SceneManager.LoadScene(0);
            }
            else
            {
                Debug.Log("User Creation Failed: " + www.error);
            }
        }
    }

    public void VerifuInputs()
    {
        SubmitButton.interactable = (Player_Name.text.Length >= 3 && Email.text.Length != 0 && ComfirmPassword.text == Password.text && Password.text.Length >= 6);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
