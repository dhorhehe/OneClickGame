using UnityEngine;
using System.Collections.Generic;

public class HighscoresExample : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        PrintTopScores();
    }


    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 100), "Insert new score"))
        {
            Highscores.PostHighscore("Testname", Mathf.RoundToInt(Random.Range(0, 100)));
            PrintTopScores();
        }
    }

    void PrintTopScores()
    {
        ICollection<ScoreEntry> scores = Highscores.GetBestScores();

        foreach (ScoreEntry score in scores)
        {
            Debug.Log(score.Name + " " + score.Score);
        }

    }
}
