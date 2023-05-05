using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public TMP_Text scoreText;

    private void OnEnable()
    {
        ScoreController.OnScoreChanged += OnScoreChanged;
    }
    private void OnDisable()
    {
        ScoreController.OnScoreChanged -= OnScoreChanged;
    }
    public void OnScoreChanged(int score)
    {
        scoreText.text = score.ToString();
    }
}
