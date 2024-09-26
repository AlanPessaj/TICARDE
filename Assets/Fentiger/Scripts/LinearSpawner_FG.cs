using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearSpawner_FG : MonoBehaviour
{
    public GameObject thing;
    public Generator_FG generator;
    float timer = 0;
    public float spawnRate;
    public bool changedSide = false;
    public bool randomSpawnSide;
    public int difficulty;
    float time;

    void Start()
    {
        time = (spawnRate - generator.difficulty / 10);
        difficulty = generator.difficulty;
        timer = Random.Range(0f, 1f);
        if (randomSpawnSide)
        {
            changedSide = Random.Range(0, 2) == 1;
        }
        else
        {
            changedSide = generator.side;
            generator.side = !generator.side;
        }
    }

    void Update()
    {
        if (timer <= 0)
        {
            if (changedSide && gameObject.name == "Cars(Clone)")
            {
                Instantiate(thing, transform.position + new Vector3(0, -2.7f, 16.5f), Quaternion.Euler(thing.transform.eulerAngles + new Vector3(0, 180, 0)), transform).GetComponent<LinearMover_FG>().spawner = this;
            }
            else if(changedSide)
            {
                Instantiate(thing, transform.position + new Vector3(0, -2.7f, 16.5f), thing.transform.rotation, transform).GetComponent<LinearMover_FG>().spawner = this;
            }
            else
            {
                Instantiate(thing, transform.position + new Vector3(0, -2.7f, -16.5f), thing.transform.rotation, transform).GetComponent<LinearMover_FG>().spawner = this;
            }
            timer = Random.Range(time/4, time);
        }
        timer -= Time.deltaTime;
    }

    private void Awake()
    {
        generator = GameObject.Find("GAMEMANAGER").GetComponent<Generator_FG>();
    }
}