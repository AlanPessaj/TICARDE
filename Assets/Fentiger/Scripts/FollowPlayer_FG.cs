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
    public float speed = 3;
    public GameObject ghost;
    // Start is called before the first frame update
    void Start()
    {
        generator = GameObject.Find("GAMEMANAGER").GetComponent<Generator_FG>();
        agent = GetComponent<NavMeshAgent>();
        if (generator.multiplayer)
        {
            players[0] = GameObject.Find("Player1");
            players[1] = GameObject.Find("Player2");
        }
        else if (generator.player1Alive)
        {
            players[0] = GameObject.Find("Player1");
        }
        else
        {
            players[1] = GameObject.Find("Player2");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (generator.multiplayer)
        {
            if ((players[0].transform.position.x >= transform.position.x - 5 && players[0].transform.position.x <= transform.position.x + 5) || (players[1].transform.position.x >= transform.position.x - 5 && players[1].transform.position.x <= transform.position.x + 5))
            {
                GetComponent<NavMeshAgent>().speed = speed;
                if (Vector3.Distance(players[0].transform.position, transform.position) < Vector3.Distance(players[1].transform.position, transform.position) && players[0].transform.parent == null)
                {
                    agent.destination = players[0].transform.position;
                }
                else if(players[1].transform.parent == null)
                {
                    agent.destination = players[1].transform.position;
                }
                else if (leftSpawn)
                {
                    GetComponent<NavMeshAgent>().speed = speed / 2;
                    agent.destination = transform.parent.position - new Vector3(0, 0, 11);
                }
                else
                {
                    GetComponent<NavMeshAgent>().speed = speed / 2;
                    agent.destination = transform.parent.position - new Vector3(0, 0, -11);
                }
            }
        }

        if (!generator.multiplayer && generator.player1Alive)
        {
            if (players[0].transform.position.x >= transform.position.x - 5 && players[0].transform.position.x <= transform.position.x + 5 && players[0].transform.parent == null)
            {
                GetComponent<NavMeshAgent>().speed = speed;
                agent.destination = players[0].transform.position;
            }
            else if (leftSpawn)
            {
                GetComponent<NavMeshAgent>().speed = speed / 2;
                agent.destination = transform.parent.position - new Vector3(0, 0, 11);
            }
            else
            {
                GetComponent<NavMeshAgent>().speed = speed / 2;
                agent.destination = transform.parent.position - new Vector3(0, 0, -11);
            }
        }
        else if(!generator.multiplayer && !generator.player1Alive)
        {
            if (players[1].transform.position.x >= transform.position.x - 5 && players[1].transform.position.x <= transform.position.x + 5 && players[1].transform.parent == null)
            {
                GetComponent<NavMeshAgent>().speed = speed;
                agent.destination = players[1].transform.position;
            }
            else if (leftSpawn)
            {
                GetComponent<NavMeshAgent>().speed = speed / 2;
                agent.destination = transform.parent.position - new Vector3(0, 0, 11);
            }
            else
            {
                GetComponent<NavMeshAgent>().speed = speed / 2;
                agent.destination = transform.parent.position - new Vector3(0, 0, -11);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Seagull"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Instantiate(ghost, transform.position, Quaternion.identity).GetComponent<DieScript_FG>().playerGhost = false;
    }
}