using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameInputManager : MonoBehaviour
{
    public bool multiplayer;
    public string player1Name;
    public string player2Name;
    public GameObject player1;
    public GameObject player2;
    // Start is called before the first frame update
    void Update()
    {
        Debug.Log(player1.GetComponent<RectTransform>().anchoredPosition.ToString() + player2.GetComponent<RectTransform>().anchoredPosition.ToString());
        if (multiplayer)
        {
            player1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -70);
            player2.SetActive(true);
            player1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -70);
        }
        else
        {
            player2.SetActive(false);
            player1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 70);
        }
    }
}
