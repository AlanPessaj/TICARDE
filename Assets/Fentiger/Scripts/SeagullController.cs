using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullController : MonoBehaviour
{
    bool firstTime;
    public bool leftSide;
    public GameObject[] players = new GameObject[2];
    void Start()
    {
        players[0] = GameObject.Find("Player1");
        players[1] = GameObject.Find("Player2");
        leftSide = Random.Range(1, 3) == 1;
        if (!leftSide)
        {
            transform.Rotate(0, 180, 0);
            transform.Translate(0, 0, 36);
        }
    }

    void Update()
    {
        if (firstTime && (players[0].transform.position.x == transform.position.x-3 || players[1].transform.position.x == transform.position.x - 3))
        {
            firstTime = false;
            gameObject.GetComponent<LinearMover_FG>().enabled = true;
        }
    }
}
