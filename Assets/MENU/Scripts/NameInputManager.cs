using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
                joinButton.color = new Color(1, 1, 1, 0);
                if (player1.GetComponent<NameController>().finished && player2.GetComponent<NameController>().finished)
                {
                    instance = StartCoroutine(Finish());
                }
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
            }
        }
    }

    IEnumerator Finish()
    {
        Debug.Log("empezo");
        yield return new WaitForSeconds(10);
        Debug.Log("temino");
    }
}
