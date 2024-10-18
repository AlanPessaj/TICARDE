using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer_FG : MonoBehaviour
{
    GameObject[] players = new GameObject[2];
    public NavMeshAgent agent;
    public bool leftSpawn;
    public Generator_FG generator;
    public float minDistance = 5f;

    void Start()
    {
        generator = GameObject.Find("GAMEMANAGER").GetComponent<Generator_FG>();
        agent = GetComponent<NavMeshAgent>();
        
        if (generator.multiplayer)
        {
            players[0] = GameObject.Find("Player1");
            players[1] = GameObject.Find("Player2");
        }
        else
        {
            if (generator.player1Alive)
            {
                players[0] = GameObject.Find("Player1");
            }
            else
            {
                players[1] = GameObject.Find("Player2");
            }
        }
    }

    void Update()
    {
        if (generator.multiplayer)
        {
            if (players[0] != null && players[1] != null)
            {
                if ((players[0].transform.position.x >= transform.position.x - 5 && players[0].transform.position.x <= transform.position.x + 5) ||
                    (players[1].transform.position.x >= transform.position.x - 5 && players[1].transform.position.x <= transform.position.x + 5))
                {
                    agent.speed = 3;
                    
                    if (Vector3.Distance(players[0].transform.position, transform.position) < Vector3.Distance(players[1].transform.position, transform.position))
                    {
                        agent.destination = players[0].transform.position;
                    }
                    else
                    {
                        agent.destination = players[1].transform.position;
                    }
                }
                else
                {
                    agent.speed = 1.5f;
                    if (leftSpawn)
                    {
                        agent.destination = transform.parent.position - new Vector3(0, 0, 11);
                    }
                    else
                    {
                        agent.destination = transform.parent.position - new Vector3(0, 0, -11);
                    }
                }
            }
        }
        else
        {
            GameObject alivePlayer = players[0] != null ? players[0] : players[1];

            if (alivePlayer != null)
            {
                float distanceToPlayer = Vector3.Distance(alivePlayer.transform.position, transform.position);

                if (distanceToPlayer > minDistance)
                {
                    agent.speed = 3;
                    agent.destination = alivePlayer.transform.position;
                }
                else
                {
                    agent.speed = 0;
                }
            }
            else
            {
                agent.speed = 1.5f;
                if (leftSpawn)
                {
                    agent.destination = transform.parent.position - new Vector3(0, 0, 11);
                }
                else
                {
                    agent.destination = transform.parent.position - new Vector3(0, 0, -11);
                }
            }
        }
    }
}
