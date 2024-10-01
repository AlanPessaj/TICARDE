using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    GameObject[] players = new GameObject[2];
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        players[0] = GameObject.Find("Player1");
        players[1] = GameObject.Find("Player2");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(players[0].transform.position, transform.position) < Vector3.Distance(players[1].transform.position, transform.position))
        {
            agent.destination = players[0].transform.position;
        }
        else
        {
            agent.destination = players[1].transform.position;
        }
    }
}
