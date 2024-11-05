using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearSpawner_FG : MonoBehaviour
{
    public GameObject thing;
    public GameObject hippo;
    public Generator_FG generator;
    float timer = 0;
    public bool changedSide = false;
    public bool randomSpawnSide;
    float initialSpawnRate;
    public float time;
    float intervalTime;

    void Start()
    {
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
        bool hippoSpawn = (Random.Range(1,201) == 1) && generator.Level > 3;

        if (gameObject.name.Contains("Logs"))
        {
            initialSpawnRate = generator.Levels[generator.Level].spawnRate[0];
        }
        else if (gameObject.name.Contains("LilyPads"))
        {
            initialSpawnRate = generator.Levels[generator.Level].spawnRate[1];
        }
        else
        {
            initialSpawnRate = generator.Levels[generator.Level].spawnRate[2];
        }
        if (timer <= 0)
        {
            if (changedSide && gameObject.name == "Cars(Clone)")
            {
                Instantiate(thing, transform.position + new Vector3(0, -2.7f, 16.5f), Quaternion.Euler(thing.transform.eulerAngles + new Vector3(0, 180, 0)), transform).GetComponent<LinearMover_FG>().spawner = this;
            }
            else if (gameObject.name == "Cars(Clone)")
            {
                Instantiate(thing, transform.position + new Vector3(0, -2.7f, -16.5f), thing.transform.rotation, transform).GetComponent<LinearMover_FG>().spawner = this;
            }
            else if (changedSide)
            {
                if (hippoSpawn)
                {
                    Instantiate(hippo, transform.position + new Vector3(0, -2.7f, 16.5f), Quaternion.Euler(0, 0, 90), transform).GetComponent<LinearMover_FG>().spawner = this;
                }
                else
                {
                    Instantiate(thing, transform.position + new Vector3(0, -2.7f, 16.5f), thing.transform.rotation, transform).GetComponent<LinearMover_FG>().spawner = this;
                }
            }
            else
            {
                if (hippoSpawn)
                {
                    Instantiate(hippo, transform.position + new Vector3(0, -2.7f, -16.5f), Quaternion.Euler(0, 180, 90), transform).GetComponent<LinearMover_FG>().spawner = this;
                }
                else
                {
                    Instantiate(thing, transform.position + new Vector3(0, -2.7f, -16.5f), thing.transform.rotation, transform).GetComponent<LinearMover_FG>().spawner = this;
                }
            }
            timer = Random.Range(initialSpawnRate, initialSpawnRate*1.25f);
        }
        timer -= Time.deltaTime;
    }


    private void Awake()
    {
        generator = GameObject.Find("GAMEMANAGER").GetComponent<Generator_FG>();
    }
}