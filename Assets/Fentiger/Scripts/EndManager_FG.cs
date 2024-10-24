﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndManager_FG : MonoBehaviour
{
    public string player1Name;
    public string player2Name;
    public float player1Score;
    public float player2Score;
    public GameObject player1Canvas;
    public GameObject player2Canvas;

     public float topScore;


    public void UpdateValues()
    {
        player1Canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = player1Name;
        player1Canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "SCORE: " + player1Score.ToString();
        player2Canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = player2Name;
        player2Canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "SCORE: " + player2Score.ToString();
    }

    public void Menu()
    {
        SceneManager.LoadScene("MENU");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game(FG)");
    }
}
