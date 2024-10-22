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
    float initialSpawnRate;
    public float time;
    float intervalTime;

    void Start()
    {
        initialSpawnRate = spawnRate;
        difficulty = generator.difficulty + 25;
        time = Mathf.Clamp((spawnRate - difficulty / 10), 0.1f, Mathf.Infinity);
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
        intervalTime = Mathf.Clamp(Random.Range(time - time/6, time + time/6), initialSpawnRate / 2.5f, Mathf.Infinity);
        if (timer <= 0)
        {
            if (changedSide && gameObject.name == "Cars(Clone)")
            {
                Instantiate(thing, transform.position + new Vector3(0, -2.7f, 16.5f), Quaternion.Euler(thing.transform.eulerAngles + new Vector3(0, 180, 0)), transform).GetComponent<LinearMover_FG>().spawner = this;
            }
            else if (changedSide)
            {
                Instantiate(thing, transform.position + new Vector3(0, -2.7f, 16.5f), thing.transform.rotation, transform).GetComponent<LinearMover_FG>().spawner = this;
            }
            else
            {
                Instantiate(thing, transform.position + new Vector3(0, -2.7f, -16.5f), thing.transform.rotation, transform).GetComponent<LinearMover_FG>().spawner = this;
            }
            if (intervalTime < 2.5f)
            {
                timer = Random.Range(2f, 2.5f);
            }
            else
            {
                timer = intervalTime;
            }
        }
        timer -= Time.deltaTime;
    }

    private void Awake()
    {
        generator = GameObject.Find("GAMEMANAGER").GetComponent<Generator_FG>();
    }
}