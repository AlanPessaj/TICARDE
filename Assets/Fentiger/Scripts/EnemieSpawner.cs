using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieSpawner : MonoBehaviour
{
    public Generator_FG generator;
    public GameObject enemie;
    bool firstTime = true;
    GameObject[] players = new GameObject[2];
    bool spawn;

    void Start()
    {
        spawn = Random.Range(0, 2) == 1;
    }

    void Update()
    {
        if (spawn && firstTime)
        {
            if (Random.Range(0, 2) == 1)
            {
                Instantiate(enemie, new Vector3(transform.position.x, -2, 12), Quaternion.identity, transform).GetComponent<FollowPlayer>().leftSpawn = true;
            }
            else
            {
                Instantiate(enemie, new Vector3(transform.position.x, -2, -12), Quaternion.identity, transform);
            }
            firstTime = false;
        }
    }

    private void Awake()
    {
        generator = GameObject.Find("GAMEMANAGER").GetComponent<Generator_FG>();
        players[0] = GameObject.Find("Player1");
        players[1] = GameObject.Find("Player2");
    }
}
