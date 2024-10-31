using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameInputManager : MonoBehaviour
{
    public RectTransform player1;
    public RectTransform player2;
    public bool multiplayer;

    void Update()
    {
        if (multiplayer)
        {
            player1.anchoredPosition = new Vector2(-280, -80);
            player2.gameObject.SetActive(true);
        }
        else
        {
            player1.anchoredPosition = new Vector2(0, -80);
            player2.gameObject.SetActive(false);
        }
    }
}
