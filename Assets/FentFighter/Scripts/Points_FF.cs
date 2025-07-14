using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Points_FF : MonoBehaviour
{
    public TextMeshProUGUI txtPlayer1Score;
    public TextMeshProUGUI txtPlayer2Score;
    public TextMeshProUGUI txtWinner;

    // Start is called before the first frame update
    private void Start()
    {
        GAMEMANAGER.Instance.GetComponent<conexion>().SendMessagestoArduino("0", new string[]{ "" });
        txtPlayer1Score.text = GameData.name1 + " SCORE: " + GameData.score1.ToString();
        txtPlayer2Score.text = GameData.name2 + " SCORE: " + GameData.score2.ToString();
        txtWinner.text = (GameData.p1Winner ? GameData.name1 : GameData.name2) + " WINS!";
        ScoreboardManager.SaveNewScore(GameData.name1, GameData.score1, "FF");
        ScoreboardManager.SaveNewScore(GameData.name2, GameData.score2, "FF");
    }

    public void Next()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game(FF)");
    }
}
