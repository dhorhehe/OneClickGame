using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class HighscoresExample : MonoBehaviour
{
    public GameObject CanvasLeader;
    public GameObject[] LeaderNames;
    public GameObject[] LeaderScores;

    void Start ()
    {
        PrintBestScore();
        PrintTopScores();
    }


    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(0, 0, 100, 100), "Insert new score"))
    //    {
    //        StartCoroutine(Highscores.PostHighscore("Testname", Mathf.RoundToInt(Random.Range(0, 100))));
    //        PrintTopScores();
    //    }
    //}

    void PrintTopScores()
    {
        ICollection<ScoreEntry> scores = Highscores.GetBestScores();

        foreach (ScoreEntry score in scores)
        {
            Debug.Log("#" + score.Position + " " + score.Name + " " + score.Score);
            LeaderNames[score.Position-1].GetComponent<Text>().text = score.Name;
            LeaderScores[score.Position-1].GetComponent<Text>().text = score.Score.ToString();
        }
    }

    void PrintBestScore(string name = "Jonas")
    {
        ScoreEntry score = Highscores.GetBestByUser(name);
        //Debug.Log("Best score for " + name + ":");
        //Debug.Log("#" + score.Position + " " + score.Name + " " + score.Score);
    }

    public void OpenLeaderboardMenu()
    {
        CanvasLeader.SetActive(true);

        PrintTopScores();
    }

    public void CloseLeaderBoard()
    {
        CanvasLeader.SetActive(false);
    }
}
