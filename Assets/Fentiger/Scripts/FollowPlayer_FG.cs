using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer_FG : MonoBehaviour
{
    public GameObject[] players = new GameObject[2];
    public NavMeshAgent agent;
    public bool leftSpawn;
    public Generator_FG generator;
    public float speed = 3;
    public GameObject ghost;
    bool soundPlayed;
    float timer = 0;
    float patrolTimer = 5f;
    bool changePatrol;
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

    void Update()
    {
        patrolTimer -= Time.deltaTime;
        if (patrolTimer <= 0)
        {
            changePatrol = !changePatrol;
            patrolTimer = 5f;
        }
        timer = Mathf.Max(0, timer - Time.deltaTime);
        if (generator.multiplayer)
        {
            if ((players[0].transform.position.x >= transform.position.x - 5 && players[0].transform.position.x <= transform.position.x + 5) || (players[1].transform.position.x >= transform.position.x - 5 && players[1].transform.position.x <= transform.position.x + 5))
            {
                if (Vector3.Distance(players[0].transform.position, transform.position) < Vector3.Distance(players[1].transform.position, transform.position) && players[0].transform.parent == null)
                {
                    if (!soundPlayed && timer == 0)
                    {
                        GetComponent<AudioSource>().Play();
                        timer = 2;
                        soundPlayed = true;
                    }
                    GetComponent<NavMeshAgent>().speed = speed;
                    agent.destination = players[0].transform.position;
                    GetComponent<Animator>().SetBool("running", true);
                    GetComponent<Animator>().SetBool("walking", false);
                }
                else if(players[1].transform.parent == null)
                {
                    if (!soundPlayed && timer == 0)
                    {
                        GetComponent<AudioSource>().Play();
                        timer = 2;
                        soundPlayed = true;
                    }
                    GetComponent<NavMeshAgent>().speed = speed;
                    GetComponent<Animator>().SetBool("running", true);
                    GetComponent<Animator>().SetBool("walking", false);
                    agent.destination = players[1].transform.position;
                }
                else if (leftSpawn)
                {
                    GetComponent<Animator>().SetBool("running", false);
                    GetComponent<Animator>().SetBool("walking", agent.velocity.magnitude > 0.1f);
                    GetComponent<NavMeshAgent>().speed = speed / 2;
                    if (changePatrol) agent.destination = transform.parent.position - new Vector3(0, 0, -11);
                    else agent.destination = transform.parent.position - new Vector3(0, 0, 11);
                    soundPlayed = false;
                }
                else
                {
                    GetComponent<Animator>().SetBool("running", false);
                    GetComponent<Animator>().SetBool("walking", agent.velocity.magnitude > 0.1f);
                    GetComponent<NavMeshAgent>().speed = speed / 2;
                    if (changePatrol) agent.destination = transform.parent.position - new Vector3(0, 0, 11);
                    else agent.destination = transform.parent.position - new Vector3(0, 0, -11);
                    soundPlayed = false;
                }
            }
            else
            {
                GetComponent<Animator>().SetBool("running", false);
                GetComponent<Animator>().SetBool("walking", agent.velocity.magnitude > 0.1f);
            }
        }

        if (!generator.multiplayer && generator.player1Alive)
        {
            if (players[0].transform.position.x >= transform.position.x - 5 && players[0].transform.position.x <= transform.position.x + 5 && players[0].transform.parent == null)
            {
                if (!soundPlayed && timer == 0)
                {
                    GetComponent<AudioSource>().Play();
                    timer = 2;
                    soundPlayed = true;
                }
                GetComponent<NavMeshAgent>().speed = speed;
                agent.destination = players[0].transform.position;
                GetComponent<Animator>().SetBool("running", true);
                GetComponent<Animator>().SetBool("walking", false);
            }
            else if (leftSpawn)
            {
                GetComponent<NavMeshAgent>().speed = speed / 2;
                if (changePatrol) agent.destination = transform.parent.position - new Vector3(0, 0, -11);
                else agent.destination = transform.parent.position - new Vector3(0, 0, 11);
                GetComponent<Animator>().SetBool("running", false);
                GetComponent<Animator>().SetBool("walking", agent.velocity.magnitude > 0.1f);
                soundPlayed = false;
            }
            else
            {
                GetComponent<NavMeshAgent>().speed = speed / 2;
                if (changePatrol) agent.destination = transform.parent.position - new Vector3(0, 0, 11);
                else agent.destination = transform.parent.position - new Vector3(0, 0, -11);
                GetComponent<Animator>().SetBool("running", false);
                GetComponent<Animator>().SetBool("walking", agent.velocity.magnitude > 0.1f);
                soundPlayed = false;
            }
        }
        else if(!generator.multiplayer && !generator.player1Alive)
        {
            if (players[1].transform.position.x >= transform.position.x - 5 && players[1].transform.position.x <= transform.position.x + 5 && players[1].transform.parent == null)
            {
                if (!soundPlayed && timer == 0)
                {
                    GetComponent<AudioSource>().Play();
                    timer = 2;
                    soundPlayed = true;
                }
                GetComponent<NavMeshAgent>().speed = speed;
                agent.destination = players[1].transform.position;
                GetComponent<Animator>().SetBool("running", true);
                GetComponent<Animator>().SetBool("walking", false);
            }
            else if (leftSpawn)
            {
                GetComponent<NavMeshAgent>().speed = speed / 2;
                if (changePatrol) agent.destination = transform.parent.position - new Vector3(0, 0, -11);
                else agent.destination = transform.parent.position - new Vector3(0, 0, 11);
                GetComponent<Animator>().SetBool("running", false);
                GetComponent<Animator>().SetBool("walking", agent.velocity.magnitude > 0.1f);
                soundPlayed = false;
            }
            else
            {
                GetComponent<NavMeshAgent>().speed = speed / 2;
                if (changePatrol) agent.destination = transform.parent.position - new Vector3(0, 0, 11);
                else agent.destination = transform.parent.position - new Vector3(0, 0, -11);
                GetComponent<Animator>().SetBool("running", false);
                GetComponent<Animator>().SetBool("walking", agent.velocity.magnitude > 0.1f);
                soundPlayed = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Seagull"))
        {
            generator.GetComponent<SoundManager_FG>().PlaySound(generator.GetComponent<SoundManager_FG>().laserImpact);
            Instantiate(ghost, transform.position, Quaternion.identity).GetComponent<DieScript_FG>().playerGhost = false;
            Destroy(gameObject);
        }
    }
}