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

    // Start is called before the first frame update
    private void Start()
    {
        GAMEMANAGER.Instance.GetComponent<conexion>().SendMessagestoArduino("0", null);
        txtPlayer1Score.text = GameData.score1.ToString();
        txtPlayer2Score.text = GameData.score2.ToString();
        txtPlayer1Name.text = GameData.name1;
        txtPlayer2Name.text = GameData.name2;
        txtWinner.text = GameData.p1Winner ? GameData.name1 : GameData.name2;
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
