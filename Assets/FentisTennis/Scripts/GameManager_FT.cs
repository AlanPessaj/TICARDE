using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_FT : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public ShotManager_FT shotmanager;
    public BallMover_FT ballmover;
    public int serve = 0;
    // Start is called before the first frame update
    void Start()
    {
        Serve(player1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Serve(GameObject player)
    {
        if(player == player1)
        {
            serve = 1;
        }
        else
        {
            serve = 2;
        }
        player1.transform.position = new Vector3(-50, 4, -30);
        player2.transform.position = new Vector3(50, 4, 30);
        ballmover.active = false;
        ballmover.transform.parent = player.transform;
        ballmover.transform.localPosition = new Vector3(1.5f, 0, 0);



        //salida
        //serve = 0;
        //ballmover.active = true;
        //ballmover.transform.parent = null;
    }
}
