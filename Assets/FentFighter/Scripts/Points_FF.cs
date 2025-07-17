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
    public GameObject insertCoin;

    // Start is called before the first frame update
    private void Start()
    {
        GAMEMANAGER.Instance.txtCredits.rectTransform.position = new Vector2(512, 159);
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
        if (GameData.credits < 1)
        {
            GAMEMANAGER.Instance.txtCredits.rectTransform.position = new Vector2(862, 59);
            StartCoroutine(InsertCoinBlink());
            return;
        }
        SceneManager.LoadScene("Game(FF)");
    }

    public IEnumerator InsertCoinBlink()
    {
        if (GAMEMANAGER.Instance.insufficientCreditsActive) yield break;
        StartCoroutine(GAMEMANAGER.Instance.InsufficientCredits(true));
        for (int i = 0; i < 3; i++)
        {
            insertCoin.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            insertCoin.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
