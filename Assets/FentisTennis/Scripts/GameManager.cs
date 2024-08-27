using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;
    public int serve = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Serve(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Serve(bool isPlayer1)
    {
        if(isPlayer1)
        {
            serve = 1;
        }
        else
        {
            serve = 2;
        }
        player1.transform.position = new Vector3(-50, 4, -30);
        player2.transform.position = new Vector3(50, 4, 30);

        //salida
        //serve = 0;
    }

}
