using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner_FG : MonoBehaviour
{
    public Generator_FG generator;
    public GameObject enemy;
    bool firstTime = true;
    bool spawn;

    void Start()
    {
        spawn = Random.Range(0, 2) == 1;
    }

    void Update()
    {
        if (firstTime)
        {
            if (enemy.layer == LayerMask.NameToLayer("Transport"))
            {
                if (Random.Range(0, 2) == 1)
                {
                    Instantiate(enemy, new Vector3(transform.position.x, -2.4f, 10), Quaternion.identity, transform).GetComponent<FrogController_FG>().leftSpawn = true;
                }
                else
                {
                    Instantiate(enemy, new Vector3(transform.position.x, -2.4f, -10), Quaternion.identity, transform);
                }
            }
            else if (spawn)
            {
                if (Random.Range(0, 2) == 1)
                {
                    Instantiate(enemy, new Vector3(transform.position.x, -2, 7), Quaternion.identity, transform).GetComponent<FollowPlayer_FG>().leftSpawn = true;
                }
                else
                {
                    Instantiate(enemy, new Vector3(transform.position.x, -2, -7), Quaternion.identity, transform);
                }
            }
            firstTime = false;
        }
    }

    private void Awake()
    {
        generator = GameObject.Find("GAMEMANAGER").GetComponent<Generator_FG>();
    }
}
