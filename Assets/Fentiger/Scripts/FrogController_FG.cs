using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController_FG : MonoBehaviour
{
    // Movimiento de 2 a 3 tiles por direccion cardinal
    GameObject[] players = new GameObject[2];
    int distance;
    int direction;
    Vector3 pos;
    public bool takenSpot;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        do
        {
            distance = Random.Range(2, 4);
            direction = Random.Range(0, 4);
            switch (direction)
            {
                case 0:
                    pos = transform.forward * distance;
                    break;
                case 1:
                    pos = transform.right * distance;
                    break;
                case 2:
                    pos = transform.forward * -distance;
                    break;
                case 3:
                    pos = transform.right * -distance;
                    break;
            }
        }
        while (!FreeSpot(pos));

        //Saltar hacia la direccion
    }

    bool FreeSpot(Vector3 pos)
    {
        //Mover trigger a pos
        // devolver si hay algo
        transform.GetChild(1).localPosition = pos;
        if (!takenSpot)
        {
            return true;
        }
        else
        {
            transform.GetChild(1).localPosition = new Vector3();
            return false;
        }
    }
    private void Awake()
    {
        players[0] = GameObject.Find("Player1");
        players[1] = GameObject.Find("Player2");
    }

}
