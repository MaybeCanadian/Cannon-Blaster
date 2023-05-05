using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreController
{
    #region Event Dispatchers
    public delegate void ScoreEvent(int value);
    public static ScoreEvent OnScoreChanged;
    #endregion
    public static int score = 0;

    public static void ResetScore()
    {
        score = 0;

        OnScoreChanged?.Invoke(score);
    }
    public static void AddScore(int value)
    {
        score += value;

        OnScoreChanged?.Invoke(score);
    }
    public static int GetScore()
    {
        return score;
    }
}
