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


    void SaveNewScore(string name, int score, string game)
    {
        SortedList<int, string> scoreboard = new SortedList<int, string>();
        List<int> sameScores = new List<int>();
        List<string> sameNames = new List<string>();
        scoreboard.Add(score, name);
        for (int i = 0; i < scores.Length; i++)
        {
            if (PlayerPrefs.HasKey($"{game}/name{i}"))
            {
                if (!scoreboard.ContainsKey(PlayerPrefs.GetInt($"{game}/score{i}")))
                {
                    scoreboard.Add(PlayerPrefs.GetInt($"{game}/score{i}"), PlayerPrefs.GetString($"{game}/name{i}"));
                }
                else
                {
                    sameScores.Add(PlayerPrefs.GetInt($"{game}/score{i}"));
                    sameNames.Add(PlayerPrefs.GetString($"{game}/name{i}"));
                }
            }
            else
            {
                break;
            }
        }
        List<int> orderedScores = new List<int>();
        List<string> orderedNames = new List<string>();
        for (int i = 0; i < scoreboard.Count; i++)
        {
            nextScore:
            if (sameScores.Count == 0)
            {
                orderedScores.Add(scoreboard.Keys[i]);
                orderedNames.Add(scoreboard.Values[i]);

                //PlayerPrefs.SetInt($"{game}/score{i}", scoreboard.Keys[i]);
                //PlayerPrefs.SetString($"{game}/name{i}", scoreboard.Values[i]);
            }
            else
            {
                for (int o = 0; o < sameScores.Count; o++)
                {
                    if (sameScores[o] == scoreboard.Keys[i])
                    {
                        orderedScores.Add(sameScores[o]);
                        orderedNames.Add(sameNames[o]);
                        //PlayerPrefs.SetInt($"{game}/score{i}", item);
                        //PlayerPrefs.SetString($"{game}/name{i}", sameScores[item]);
                        sameScores.RemoveAt(o);
                        sameNames.RemoveAt(o);
                        goto nextScore;
                    }
                }
                orderedScores.Add(scoreboard.Keys[i]);
                orderedNames.Add(scoreboard.Values[i]);
                //PlayerPrefs.SetInt($"{game}/score{i}", scoreboard.Keys[i]);
                //PlayerPrefs.SetString($"{game}/name{i}", scoreboard.Values[i]);
            }
        }
        int p = orderedScores.Count - 1;
        for (int i = 0; i < orderedScores.Count && i < scores.Length; i++)
        {
            PlayerPrefs.SetInt($"{game}/score{i}", orderedScores[p]);
            PlayerPrefs.SetString($"{game}/name{i}", orderedNames[p]);
            p--;
        }
        PlayerPrefs.Save();
    }
}
