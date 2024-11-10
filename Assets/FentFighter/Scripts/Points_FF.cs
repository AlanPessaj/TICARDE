using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Points_FF : MonoBehaviour
{
    public TextMeshProUGUI txtPlayer1Score;
    public TextMeshProUGUI txtPlayer2Score;
    public TextMeshProUGUI txtPlayer1Name;
    public TextMeshProUGUI txtPlayer2Name;
    public TextMeshProUGUI txtWinner;
    conexion conexion;
    int player1Score;
    int player2Score;

    private void Start()
    {
        conexion = GAMEMANAGER.Instance.GetComponent<conexion>();
        conexion.SendMessagestoArduino("0", null);
    }
    // Start is called before the first frame update
    public void UpdateScore(float player1Score, float player2Score, string winner)
    {
        this.player1Score = (int)player1Score;
        this.player2Score = (int)player2Score;
        txtPlayer1Score.text = player1Score.ToString();
        txtPlayer2Score.text = player2Score.ToString();
        txtPlayer1Name.text = GameData.name1;
        txtPlayer2Name.text = GameData.name2;
        txtWinner.text = winner;
        ScoreboardManager.SaveNewScore(GameData.name1, (int)player1Score, "FF");
        ScoreboardManager.SaveNewScore(GameData.name2, (int)player2Score, "FF");
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
