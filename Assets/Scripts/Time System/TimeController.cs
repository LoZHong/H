using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class CurrentTime
{
    public int CurrentYear;
    public int CurrentMonth;
    public int CurrentDay;
    public int CurrentHour;
    public int CurrentMinute;
    public int CurrentSecond;
    public int SaveYear;
}

public class TimeController : MonoBehaviour
{

    public bool RunTimer;
    public CurrentTime currentTime;

    [Header("Json")]
    public TextAsset SampleTimeJson;
    public string JsonTime;
    public string JsonTimeFilePath;
    public bool isDone;

    // Start is called before the first frame update
    void Start()
    {
        JsonTimeFilePath = Path.Combine(Application.persistentDataPath, "Time.json");
        isPathExists(JsonTimeFilePath);

        RunTimer = true;
    }

    public void isPathExists(string FilePath)
    {
        if (File.Exists(FilePath)) { GetSavedTime(); }
        else { GeneratePath(); }
    }
    public void GetSavedTime()
    {

        JsonTime = File.ReadAllText(JsonTimeFilePath);
        currentTime = JsonUtility.FromJson<CurrentTime>(JsonTime);
        isDone = true;
    }

    public void GeneratePath()
    {
        FileStream fileStream = File.Create(JsonTimeFilePath);
        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(SampleTimeJson.text);
        }
        GetSavedTime();
    }

    public void updateJson()
    {
        File.WriteAllText(JsonTimeFilePath, JsonUtility.ToJson(currentTime));
    }

    // Update is called once per frame
    void Update()
    {
        if(RunTimer == true && isDone == true)
        {
            UpdateYear();
            OnTimerStart();
        }
    }

    public void UpdateYear()
    {
        if (currentTime.SaveYear != DateTime.Today.Year) 
        { 
            currentTime.CurrentYear += 1;
            currentTime.SaveYear = DateTime.Today.Year;
        }
    }

    public void OnTimerStart()
    {
        currentTime.CurrentSecond += 1;
        UnityEngine.Debug.Log("CurrentTime is"+ DateTime.Today.Year.ToString());

    }

}
