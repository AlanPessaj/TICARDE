﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Points_FT : MonoBehaviour
{
    public TextMeshProUGUI txtPlayer1Score;
    public TextMeshProUGUI txtPlayer2Score;
    public TextMeshProUGUI txtPlayer1Name;
    public TextMeshProUGUI txtPlayer2Name;
    public TextMeshProUGUI txtWinner;
    int player1Score;
    int player2Score;
    // Start is called before the first frame update
    public void UpdateScore(int player1Score, int player2Score)
    {
        this.player1Score = player1Score;
        this.player2Score = player2Score;
        txtPlayer1Score.text = player1Score.ToString();
        txtPlayer2Score.text = player2Score.ToString();
        txtPlayer1Name.text = GameData.name1;
        txtPlayer2Name.text = GameData.name2;
        if (player1Score > player2Score)
            txtWinner.text = GameData.name1;
        else
            txtWinner.text = GameData.name2;
    }

    public void Next()
    {
        ScoreboardManager.SaveNewScore(GameData.name1, player1Score, "FT");
        ScoreboardManager.SaveNewScore(GameData.name2, player2Score, "FT");
        SceneManager.LoadScene("Leaderboard");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game(FT)");
    }
}