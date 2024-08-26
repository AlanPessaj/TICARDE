using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;
    public bool serve = false;
    // Start is called before the first frame update
    void Start()
    {
        Serve();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Serve()
    {
        serve = true;
        player1.transform.position = new Vector3(-50, 4, -30);
        player2.transform.position = new Vector3(50, 4, 30);

        //salida
        //serve = false;
    }

}
