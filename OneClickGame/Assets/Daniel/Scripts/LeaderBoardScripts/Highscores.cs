using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using UnityEngine;

public class ScoreEntry
{
    public string Name { get; set; }
    public int Score { get; set; }
    public int Position { get; set; }
}

//Workaround since unity's json lib does not allow for renaming (We dont like members with lowercase)
[Serializable]
internal class ScoreEntryJSON
{
    public int position;
    public string name;
    public int score;
}

//Workaround since unity's json lib does not support collection directly (http://forum.unity3d.com/threads/how-to-load-an-array-with-jsonutility.375735/)
[Serializable]
internal class ScoreEntryCollectionJsonWrapper
{
    public ScoreEntryJSON[] data;
}

public static class Highscores
{
    private static string _apiLink = "http://jonasmhansen.com/danny/fishgame/api/v1/Scores";

    public static string ApiLink
    {
        get { return _apiLink; }
        set { _apiLink = value; }
    }

    public static ICollection<ScoreEntry> GetBestScores(int limit = 10)
    {
        ICollection<ScoreEntry> entries;
        using (WebClient wc = new WebClient())
        {
            string json = wc.DownloadString(ApiLink + "/Top/" + limit);
            entries = Deserialize(json);
        }

        return entries;
    }

    public static ScoreEntry GetBestByUser(string name)
    {
        ICollection<ScoreEntry> entries;
        using (WebClient wc = new WebClient())
        {
            string json = wc.DownloadString(ApiLink + "/Best/" + name);
            entries = Deserialize(json);
        }



        return entries.FirstOrDefault();
    } 

    public static IEnumerator PostHighscore(string name, int score)
    {
        string json = "[{\"name\":\"" + name + "\",\"score\":" + score + "}]";
        WWW www = new WWW(ApiLink, Encoding.UTF8.GetBytes(json));
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("Error downloading: " + www.error);
        }
    }

    private static ICollection<ScoreEntry> Deserialize(string json)
    {
        json = "{ \"data\": " + json + "}"; //Workaround to make unity's json lib working
        ICollection<ScoreEntryJSON> scores = JsonUtility.FromJson<ScoreEntryCollectionJsonWrapper>(json).data;
        return scores.Select(x => new ScoreEntry() {Score = x.score, Name = x.name, Position = x.position}).ToList();
    }
}
