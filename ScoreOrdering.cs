using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using System.Text;
using UnityEngine.UI;

public class ScoreOrdering : MonoBehaviour
{
    private string filePath; 
    public TextMeshProUGUI playerScoreTextAll;
    public ScrollRect scrollRect;
    
    [Serializable]
    public class PlayerScore
    {
        public string playerName;
        public int score;

        public PlayerScore(string name, int score)
        {
            playerName = name;
            this.score = score;
        }
    }
    private void Start()
    {
        filePath = Application.dataPath + "/player_data.txt"; 
        SortAndWritePlayerScores();
        DisplayAllScores();
    }

    private void SortAndWritePlayerScores()
    {
        string[] lines = File.ReadAllLines(filePath);
        
        List<PlayerScore> playerScores = new List<PlayerScore>();
        
        foreach (string line in lines)
        {
            string[] parts = line.Split(':');
            if (parts.Length == 2)
            {
                string playerName = parts[0].Trim();
                int score;
                if (int.TryParse(parts[1].Trim(), out score))
                {
                    PlayerScore playerScore = new PlayerScore(playerName, score);
                    playerScores.Add(playerScore);
                }
            }
        }
        
        playerScores.Sort(new PlayerScoreComparer());
        
        string[] sortedLines = new string[playerScores.Count];
        for (int i = 0; i < playerScores.Count; i++)
        {
            sortedLines[i] = $"{playerScores[i].playerName}:{playerScores[i].score}";
        }
        
        File.WriteAllLines(filePath, sortedLines);
        Debug.Log("Player scores have been sorted and updated in the file: player_data.txt");
    }

    private class PlayerScoreComparer : IComparer<PlayerScore>
    {
        public int Compare(PlayerScore x, PlayerScore y)
        {
            int scoreComparison = y.score.CompareTo(x.score);
            if (scoreComparison == 0)
            {
                return x.playerName.CompareTo(y.playerName);
            }
            return scoreComparison;
        }
    }

    private void DisplayAllScores()
    {
        string[] lines = File.ReadAllLines(filePath);
        Array.Sort(lines, (line1, line2) =>
        {
            string[] splitData1 = line1.Split(':');
            string[] splitData2 = line2.Split(':');
            int score1 = int.Parse(splitData1[1]);
            int score2 = int.Parse(splitData2[1]);
            return score2.CompareTo(score1);
        });

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string[] ranking = { "Gold", "Silver", "Bronze" };
        int remainingRanking = 4;

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] splitData = line.Split(':');
            string playerName = splitData[0];
            int playerScore = int.Parse(splitData[1]);

            if (i < ranking.Length)
            {
                sb.AppendLine($"{ranking[i]} Player {playerName} => {playerScore}");
            }
            else
            {
                sb.AppendLine($"{remainingRanking}th Player {playerName} => {playerScore}");
                remainingRanking++;
            }
            sb.AppendLine();
        }

        playerScoreTextAll.text = sb.ToString();
    }
}
