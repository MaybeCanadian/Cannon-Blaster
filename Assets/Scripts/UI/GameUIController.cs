using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public TMP_Text scoreText;

    private void OnEnable()
    {
        ConnectEvents();
    }
    private void OnDisable()
    {
        DisconnectEvents();
    }
    private void ConnectEvents()
    {
        ScoreController.OnScoreChanged += OnScoreChanged;
    }
    private void DisconnectEvents()
    {
        ScoreController.OnScoreChanged -= OnScoreChanged;
    }
    private void OnScoreChanged(int score)
    {
        scoreText.text = score.ToString();
    }
}
