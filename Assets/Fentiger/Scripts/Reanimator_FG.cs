﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reanimator_FG : MonoBehaviour
{
    void Update()
    {
        if (GetComponent<Generator_FG>().initialMultiplayer)
        {
            if (Input.GetButtonDown("A") || Input.GetButtonDown("B") || Input.GetButtonDown("C") && !GetComponent<Generator_FG>().multiplayer & !GetComponent<Generator_FG>().isTherePlayer1)
            {
                if (GetComponent<Generator_FG>().players[1].transform.GetChild(0).gameObject.activeSelf)
                {
                    GetComponent<Generator_FG>().players[1].transform.GetChild(0).gameObject.SetActive(false);
                    GetComponent<Generator_FG>().players[0].SetActive(true);
                    GetComponent<Generator_FG>().players[0].transform.position = GetComponent<Generator_FG>().players[1].transform.position;
                }
            }
        }

        if (GetComponent<Generator_FG>().initialMultiplayer)
        {
            if (Input.GetButtonDown("A2") || Input.GetButtonDown("B2") || Input.GetButtonDown("C2") && !GetComponent<Generator_FG>().multiplayer & !GetComponent<Generator_FG>().isTherePlayer2)
            {
                if (GetComponent<Generator_FG>().players[0].transform.GetChild(0).gameObject.activeSelf)
                {
                    GetComponent<Generator_FG>().players[0].transform.GetChild(0).gameObject.SetActive(false);
                    GetComponent<Generator_FG>().players[1].SetActive(true);
                    GetComponent<Generator_FG>().players[1].transform.position = GetComponent<Generator_FG>().players[0].transform.position;
                }
            }
        }
    }
}