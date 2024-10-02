using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController_FG : MonoBehaviour
{
    // Movimiento de 2 a 3 tiles por direccion cardinal
    GameObject[] players = new GameObject[2];
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        players[0] = GameObject.Find("Player1");
        players[1] = GameObject.Find("Player2");
    }
}
