using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Points_FF : MonoBehaviour
{
    public TextMeshProUGUI txtPlayer1Score;
    public TextMeshProUGUI txtPlayer2Score;
    public TextMeshProUGUI txtPlayer1Name;
    public TextMeshProUGUI txtPlayer2Name;
    public TextMeshProUGUI txtWinner;
    public string player1Name;
    public string player2Name;
    // Start is called before the first frame update
    void UpdateScore(float player1Score, float player2Score)
    {
        txtPlayer1Score.text = player1Score.ToString();
        txtPlayer2Score.text = player2Score.ToString();
        txtPlayer2Name.text = player1Name;
        txtPlayer2Name.text = player2Name;
        if (player1Score > player2Score)
            txtWinner.text = player1Name;
        else
            txtWinner.text = player2Name;
    }
}
