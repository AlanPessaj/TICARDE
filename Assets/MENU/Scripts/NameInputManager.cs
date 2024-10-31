using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NameInputManager : MonoBehaviour
{
    public RectTransform player1;
    public RectTransform player2;
    public bool multiplayer;
    public Image joinButton;
    public Coroutine instance;
    public string game;


    private void Start()
    {
        multiplayer = game != "FG";
    }

    void Update()
    {
        if (multiplayer)
        {
            if (instance == null)
            {
                player1.anchoredPosition = new Vector2(-280, -80);
                player2.gameObject.SetActive(true);
                joinButton.gameObject.SetActive(false);
                if (player1.GetComponent<NameController>().finished && player2.GetComponent<NameController>().finished) instance = StartCoroutine(Finish());
            }
        }
        else
        {
            if (instance == null)
            {
                joinButton.color = new Color(joinButton.color.r, joinButton.color.g, joinButton.color.b, Mathf.PingPong(Time.time * 1.5f, 1.0f));
                player1.anchoredPosition = new Vector2(0, -80);
                player2.gameObject.SetActive(false);
                if (Input.GetButtonDown("A2") || Input.GetButtonDown("B2") || Input.GetButtonDown("C2")) multiplayer = true;
                if (player1.GetComponent<NameController>().finished) instance = StartCoroutine(Finish());
            }
        }
    }

    IEnumerator Finish()
    {
        if (!multiplayer) joinButton.gameObject.SetActive(false);
        for (int i = 0; i < 10; i++)
        {
            foreach (RectTransform character in player1.GetComponent<NameController>().chars)
                character.transform.GetComponent<TextMeshProUGUI>().color = Color.white;

            if (multiplayer)
                foreach (RectTransform character in player2.GetComponent<NameController>().chars)
                    character.transform.GetComponent<TextMeshProUGUI>().color = Color.white;

            yield return new WaitForSeconds(0.1f);
            foreach (RectTransform character in player1.GetComponent<NameController>().chars)
                character.transform.GetComponent<TextMeshProUGUI>().color = Color.green;

            if (multiplayer)
                foreach (RectTransform character in player2.GetComponent<NameController>().chars)
                    character.transform.GetComponent<TextMeshProUGUI>().color = Color.green;

            yield return new WaitForSeconds(0.1f);
        }
        GameData.name1 = player1.GetComponent<NameController>().chars[0].GetComponent<TextMeshProUGUI>().text + player1.GetComponent<NameController>().chars[1].GetComponent<TextMeshProUGUI>().text + player1.GetComponent<NameController>().chars[2].GetComponent<TextMeshProUGUI>().text + player1.GetComponent<NameController>().chars[3].GetComponent<TextMeshProUGUI>().text;
        if (multiplayer)
        {
            GameData.name2 = player2.GetComponent<NameController>().chars[0].GetComponent<TextMeshProUGUI>().text + player2.GetComponent<NameController>().chars[1].GetComponent<TextMeshProUGUI>().text + player2.GetComponent<NameController>().chars[2].GetComponent<TextMeshProUGUI>().text + player2.GetComponent<NameController>().chars[3].GetComponent<TextMeshProUGUI>().text;
        }
        else GameData.name2 = "";
        GameData.multiplayer = multiplayer;
        GameData.game = game;
        SceneManager.LoadScene("CharacterSelector");
    }
}

public static class GameData
{
    public static string name1;
    public static string name2;
    public static bool multiplayer;
    public static string game;
}

