using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameInputManager : MonoBehaviour
{
    public RectTransform player1;
    public RectTransform player2;
    public bool multiplayer;
    public Image joinButton;
    float timer = 0;
    public Coroutine instance;

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
            foreach (RectTransform character in player1.GetComponent<NameController>().chars) character.transform.GetComponent<TextMeshProUGUI>().color = Color.white;
            if(multiplayer) foreach (RectTransform character in player2.GetComponent<NameController>().chars) character.transform.GetComponent<TextMeshProUGUI>().color = Color.white;
            yield return new WaitForSeconds(0.1f);
            foreach (RectTransform character in player1.GetComponent<NameController>().chars) character.transform.GetComponent<TextMeshProUGUI>().color = Color.green;
            if (multiplayer) foreach (RectTransform character in player2.GetComponent<NameController>().chars) character.transform.GetComponent<TextMeshProUGUI>().color = Color.green;
            yield return new WaitForSeconds(0.1f);
        }
        //CERRAR
    }
}
