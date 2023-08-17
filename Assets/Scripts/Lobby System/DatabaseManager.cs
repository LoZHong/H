using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager
{
    public static int PlayerId;
    public static string PlayerName;
    public static CurrentTime TimeJson;

    public static bool LoggedIn {  get { return PlayerId != 0; } }

    public static void LogOut()
    {
        PlayerId = 0;
        PlayerName = null;
    }
}
