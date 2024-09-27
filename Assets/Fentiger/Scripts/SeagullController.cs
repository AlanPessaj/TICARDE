using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullController : MonoBehaviour
{
    bool firstTime;
    public GameObject[] players = new GameObject[2];
    void Start()
    {
        
    }

    void Update()
    {
        if (firstTime && (players[0].transform.position.x == transform.position.x-3 || players[1].transform.position.x == transform.position.x - 3))
        {
            //Spawnear
        }
    }
}
