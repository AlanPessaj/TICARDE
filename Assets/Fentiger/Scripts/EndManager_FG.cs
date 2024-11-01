using System.Collections;
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


    private void Start()
    {
        GameObject.Find("TICARDEMANAGER").GetComponent<GAMEMANAGER>().enabled = true;
    }
    public void UpdateValues()
    {
        player1Canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = player1Name;
        player1Canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "SCORE: " + player1Score.ToString();
        ScoreboardManager.SaveNewScore(player1Name, (int)player1Score, "FG");
        if (player2Name != null)
        {
            ScoreboardManager.SaveNewScore(player2Name, (int)player2Score, "FG");
            player2Canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = player2Name;
            player2Canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "SCORE: " + player2Score.ToString();
        }
    }

    public void Next()
    {
        GameData.game = "FG";
        SceneManager.LoadScene("Leaderboard");
    }


    public void PlayAgain()
    {
        SceneManager.LoadScene("Game(FG)");
    }
}
