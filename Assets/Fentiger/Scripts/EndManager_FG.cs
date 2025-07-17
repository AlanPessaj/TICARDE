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
    public GAMEMANAGER ticardemanager;
    public GameObject insertCoin;


    private void Start()
    {
        player1Name = GameData.name1;
        player2Name = GameData.name2;
        player1Score = GameData.score1;
        player2Score = GameData.score2;
        ticardemanager = GameObject.Find("TICARDEMANAGER").GetComponent<GAMEMANAGER>();
        ticardemanager.enabled = true;
        ticardemanager.GetComponent<conexion>().SendMessagestoArduino("0", new string[]{ "" });
        player1Canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = player1Name + " SCORE: ";
        player1Canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text += player1Score.ToString();
        ScoreboardManager.SaveNewScore(player1Name, (int)player1Score, "FG");
        if (player2Name != "")
        {
            ScoreboardManager.SaveNewScore(player2Name, (int)player2Score, "FG");
            player2Canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = player2Name + " SCORE: ";
            player2Canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text += player2Score.ToString();
        }
        else
        {
            player2Canvas.SetActive(false);
            player1Canvas.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
    }

    public void Next()
    {
        GameData.game = "FG";
        SceneManager.LoadScene("Leaderboard");
    }


    public void PlayAgain()
    {
        if (GameData.credits < (player2Name == "" ? 1 : 2))
        {
            GAMEMANAGER.Instance.txtCredits.rectTransform.position = new Vector2(862, 59);
            StartCoroutine(InsertCoinBlink());
            return;
        }
        SceneManager.LoadScene("Game(FG)");
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
