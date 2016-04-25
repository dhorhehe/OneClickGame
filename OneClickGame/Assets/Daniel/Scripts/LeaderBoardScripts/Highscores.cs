using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using UnityEngine;

public class ScoreEntry
{
    public string Name { get; set; }
    public int Score { get; set; }
}

//Workaround since unity's json lib does not allow for renaming (We dont like members with lowercase)
[Serializable]
internal class ScoreEntryJSON
{
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
            string json = wc.DownloadString(ApiLink + "?orderBy=score&limit=" + limit);
            entries = Deserialize(json);
        }

        return entries;
    }

    public static void PostHighscore(string name, int score)
    {
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiLink);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = "[{\"name\":\"" + name + "\",\"score\":" + score + "}]";

            Debug.Log(json);

            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
        }

        httpWebRequest.GetResponse();
    }

    private static ICollection<ScoreEntry> Deserialize(string json)
    {
        json = "{ \"data\": " + json + "}"; //Workaround to make unity's json lib working
        ICollection<ScoreEntryJSON> scores = JsonUtility.FromJson<ScoreEntryCollectionJsonWrapper>(json).data;
        return scores.Select(x => new ScoreEntry() {Score = x.score, Name = x.name}).ToList();
    }
}
