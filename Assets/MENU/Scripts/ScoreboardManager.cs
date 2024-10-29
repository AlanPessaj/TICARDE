using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardManager : MonoBehaviour
{
    public Text[] names;
    public Text[] scores;
    // Start is called before the first frame update
    void Start()
    {

    }

    void LoadScores(string game)
    {
        for (int i = 0; i < names.Length; i++)
        {
            names[i].text = PlayerPrefs.GetString($"{game}/name{i}", "NONE");
        }
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i].text = PlayerPrefs.GetInt($"{game}/score{i}", 0).ToString();
        }
    }
}
