using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreControl : MonoBehaviour
{
    public float score;
    private float prevScore = -1;
    private string scoreStr;

    // Update is called once per frame
    void Update()
    {
        if(score != prevScore)
        {
            Debug.Log("Updating score");
            UpdateScore();
        }
    }

    public void AddScore(int amount)
    {
        if(scoreStr.Length <= 40)
            score += amount;
        if(scoreStr.Length > 40)
        {
            scoreStr = "";
            for (int i = 0; i < 40; i++)
                scoreStr += "9";
            score = float.Parse(scoreStr);
        }
    }

    private void UpdateScore()
    {
        if (score.ToString().Contains("E+"))
        {
            string[] scoreInfo = score.ToString().Split('E');
            Debug.Log(scoreInfo[0] + "/" + scoreInfo[1]);
            scoreStr = Mathf.RoundToInt(float.Parse(scoreInfo[0]) * 10).ToString();
            scoreInfo[1] = scoreInfo[1].Remove(0, 1);
            Debug.Log(scoreInfo[1]);
            for (int i = 0; i < int.Parse(scoreInfo[1]); i++)
                scoreStr += "0";
        }
        else
            scoreStr = score.ToString();

        prevScore = score;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect((Screen.width/2)-(scoreStr.Length*4), 10, 300, 50), scoreStr);
    }
}
