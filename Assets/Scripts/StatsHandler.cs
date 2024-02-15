using System;
using TMPro;
using UnityEngine;

[Serializable]
public class Stats
{
    public int HighestScore;
    public float BestTime;
    public int HighestLength;

    public float AverageScore;
    public float AverageTime;
    public float AverageLength;
    public int TotalGames;

    public Stats()
    {
        HighestScore = 0;
        BestTime = 0;
        HighestLength = 0;
        AverageScore = 0;
        AverageTime = 0;
        AverageLength = 0;
        TotalGames = 0;
    }

    public void PullStats()
    {
        HighestScore = PlayerPrefs.GetInt("HighestScore", 0);
        BestTime = PlayerPrefs.GetFloat("BestTime", 0);
        HighestLength = PlayerPrefs.GetInt("HighestLength", 0);
        AverageScore = PlayerPrefs.GetFloat("AverageScore", 0);
        AverageTime = PlayerPrefs.GetFloat("AverageTime", 0);
        AverageLength = PlayerPrefs.GetFloat("AverageLength", 0);
        TotalGames = PlayerPrefs.GetInt("TotalGames", 0);
    }

    public void PushStats()
    {
        PlayerPrefs.SetInt("HighestScore", HighestScore);
        PlayerPrefs.SetFloat("BestTime", BestTime);
        PlayerPrefs.SetInt("HighestLength", HighestLength);
        PlayerPrefs.SetFloat("AverageScore", AverageScore);
        PlayerPrefs.SetFloat("AverageTime", AverageTime);
        PlayerPrefs.SetFloat("AverageLength", AverageLength);
        PlayerPrefs.SetInt("TotalGames", TotalGames);
    }

    public void AddGameData(GameData gameData)
    {
        if (gameData.Score > HighestScore)
        {
            HighestScore = gameData.Score;
        }
        if (gameData.Time > BestTime)
        {
            BestTime = gameData.Time;
        }
        if (gameData.Length > HighestLength)
        {
            HighestLength = gameData.Length;
        }
        AverageScore = (AverageScore * TotalGames + gameData.Score) / (TotalGames + 1);
        AverageTime = (AverageTime * TotalGames + gameData.Time) / (TotalGames + 1);
        AverageLength = (AverageLength * TotalGames + gameData.Length) / (TotalGames + 1);
        TotalGames++;
    }
}

[Serializable]
public enum Stat
{
    HighestScore,
    BestTime,
    HighestLength,
    AverageScore,
    AverageTime,
    AverageLength,
    TotalGames,
    None
}

public class StatsHandler : MonoBehaviour
{
    public Stat stat;
    private TextMeshProUGUI tmp;

    private void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    public void Update()
    {
        switch (stat)
        {
            case Stat.HighestScore:
                tmp.text = "Highest Score: " + SingleState.Instance.stats.HighestScore;
                break;
            case Stat.BestTime:
                tmp.text = "Longest Time: " + MathF.Round(SingleState.Instance.stats.BestTime, 2) + "s";
                break;
            case Stat.HighestLength:
                tmp.text = "Longest Snake: " + SingleState.Instance.stats.HighestLength;
                break;
            case Stat.AverageScore:
                tmp.text = "Average Score: " + MathF.Round(SingleState.Instance.stats.AverageScore, 2);
                break;
            case Stat.AverageTime:
                tmp.text = "Average Time: " + MathF.Round(SingleState.Instance.stats.AverageTime, 2) + "s";
                break;
            case Stat.AverageLength:
                tmp.text = "Average Length: " + MathF.Round(SingleState.Instance.stats.AverageLength, 2);
                break;
            case Stat.TotalGames:
                tmp.text = "Total Games: " + SingleState.Instance.stats.TotalGames;
                break;

        }
    }

    public void ResetStats()
    {
        SingleState.Instance.stats = new Stats();
        SingleState.Instance.stats.PushStats();
    }
}
